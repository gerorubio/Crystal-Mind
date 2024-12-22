using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Character : MonoBehaviour {
    public CharacterSO characterSO;

    private bool isGamePaused = false;

    // Base Stats
    public int CurrentHp { get; set; }
    public float CurrentReloadSpeed { get; set; }
    public float CurrentFireRate { get; set; }
    public float CurrentMovementSpeed { get; set; }
    public float CurrentAttackRange { get; set; }
    public float CurrentCriticalChance { get; set; } = 0f;
    public float CurrentXpCollectionRange { get; set; } = 5f;

    // Weapon
    public Weapon Weapon { get; set; }

    // Artifacts
    private List<ArtifactSO> CurrentArtifacts { get; } = new List<ArtifactSO>();

    // Spell
    public SpellSO CurrentSpell { get; set; }

    public int CurrentSpellPoints {  get; set; }

    // XP Level System
    private int currentLevel = 1;
    private int currentXP = 0;
    private int xpToNextLevel = 3;

    // Invulverability
    private bool isInvulnerable = false;
    private float invulnerabilityTime = 0.5f;
    [SerializeField]
    private float repelSpeed;
    [SerializeField]
    private float repelDuration;
    [SerializeField]
    private float repelRadius;

    // UNITY EVENTS
    public event Action<int, int> OnXpChanged; // currentXP, xpToNextLevel
    public event Action<int> OnLevelUp; // currenLevel
    public event Action<SpellSO, int> OnEquipSpell; // currentSpell, currentSpellPoints
    public event Action<int> OnIncreaseSpellPoints; // currentSpell, currentSpellPoints
    public event Action<ArtifactSO> OnEquipArtifact; // artifact
    public event Action<int> OnHpChanged;

    // Getters and Setters
    public float MovementSpeed => CurrentMovementSpeed;

    void Awake() {
        if (characterSO == null) {
            Debug.LogError("Character SO not set");
        } else {
            InitializeCharacter();
        }
    }

    void Start() {
        // Event called to UI
        OnEquipSpell?.Invoke(CurrentSpell, CurrentSpellPoints);
        OnEquipArtifact?.Invoke(CurrentArtifacts.First());
        OnHpChanged?.Invoke(CurrentHp);

        Weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();

        Weapon.OnReload += IncreaseSpellPoints;
        Weapon.OnShoot += TriggerArtifactsOnShoot;
    }

    public void Update() {
        if (isGamePaused) return;
    }

    void InitializeCharacter() {
        CurrentHp = characterSO.hp;
        CurrentReloadSpeed = characterSO.reloadSpeed;
        CurrentFireRate = characterSO.fireRate;
        CurrentMovementSpeed = characterSO.movementSpeed;
        CurrentAttackRange = characterSO.attackRange;

        CurrentArtifacts.Add(characterSO.initialArtifact);

        CurrentSpell = characterSO.initialSpell;

        if (CurrentSpell == null) {
            Debug.LogWarning("Initial spell is not set in CharacterSO");
        }

        CurrentSpellPoints = 0;
    }

    public void GainXP(int xp) {
        this.currentXP += xp;

        if(this.currentXP >= xpToNextLevel) {
            this.currentXP -= xpToNextLevel;
            this.currentLevel++;
            LevelUp();

            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.25f);
        }

        OnXpChanged?.Invoke(currentXP, xpToNextLevel);
    }

    private void LevelUp() {
        GameManager.Instance.TogglePause();
        OnLevelUp?.Invoke(currentLevel); // current level will be to apply weight in upgrade rarity
    }

    private void IncreaseSpellPoints(int value) {
        CurrentSpellPoints = Mathf.Clamp(CurrentSpellPoints + value, 0, CurrentSpell.cost);
        OnIncreaseSpellPoints?.Invoke(CurrentSpellPoints);
    }

    public void EquipSpell() {
        OnEquipSpell?.Invoke(CurrentSpell, CurrentSpellPoints);
    }

    public void EquipArtifact(ArtifactSO artifact, Face face) {
        artifact.artifactEffect.OnEquip(this, Weapon, face);
        OnEquipArtifact?.Invoke(artifact);
    }

    private void TriggerArtifactsOnShoot(Weapon weapon, Dice dice, Projectile projectile) {
        foreach(ArtifactSO artifact in CurrentArtifacts) {
            artifact.artifactEffect.OnShoot(null, weapon, dice, projectile);
        }
    }

    // SPELLS
    public void CastSpell() {
        if (CurrentSpell != null) {
            if(CurrentSpellPoints == CurrentSpell.cost) {
                CurrentSpellPoints = 0;
                CurrentSpell.Cast(this);
                OnEquipSpell?.Invoke(CurrentSpell, CurrentSpellPoints);
            }
        }
    }

    // Modifiying stats
    private void TakeDamage(int damage) {
        CurrentHp -= damage;
        OnHpChanged?.Invoke(CurrentHp);
    }

    // Repel Enemies

    private void RepelEnemies(float repelSpeed, float repelDuration, float repelRadius) {
        Collider[] enemies = Physics.OverlapSphere(transform.position, repelRadius, LayerMask.GetMask("Enemy"));

        foreach (Collider enemy in enemies) {
            NavMeshAgent agent = enemy.transform.parent.GetComponent<NavMeshAgent>();
            if(agent != null) {
                Vector3 repelDirection = (enemy.transform.position - transform.position).normalized;
                Vector3 repelVelocity = repelDirection * repelSpeed;

                StartCoroutine(ApplyRepelVelocity(agent, repelVelocity, repelDuration));
            }
        }
    }

    private IEnumerator ApplyRepelVelocity(NavMeshAgent agent, Vector3 velocity, float duration) {
        float timer = 0f;

        while(timer < duration) {
            timer += Time.deltaTime;
            agent.velocity = velocity;
            yield return null;
        }


        agent.velocity = Vector3.zero;
    }

    // Collisions
    private void OnTriggerEnter(Collider other) {
        if(!isInvulnerable) {
            if (other.CompareTag("Enemy") || other.CompareTag("EnemyProjectile")) {
                TakeDamage(1);
                StartCoroutine(TurnInvulnerability());
                RepelEnemies(repelSpeed, repelDuration, repelRadius);
            }
        }
    }

    // Coroutines
    private IEnumerator TurnInvulnerability() {
        isInvulnerable = true;

        yield return new WaitForSeconds(invulnerabilityTime);

        isInvulnerable = false;
    }

    // PAUSE GAME
    private void OnEnable() {
        GameManager.OnGamePaused += HandleGamePaused;
        GameManager.OnGameResumed += HandleGameResumed;

        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null) {
            playerController.OnSpellCast+= CastSpell;
        }
    }

    private void OnDisable() {
        GameManager.OnGamePaused -= HandleGamePaused;
        GameManager.OnGameResumed -= HandleGameResumed;

        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null) {
            playerController.OnSpellCast -= CastSpell;
        }
    }

    private void HandleGamePaused() {
        isGamePaused = true;
    }

    private void HandleGameResumed() {
        isGamePaused = false;
    }
}