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
    public List<ArtifactSO> CurrentArtifacts { get; } = new List<ArtifactSO>();

    // Spell
    public SpellSO CurrentSpell { get; set; }

    public int CurrentSpellPoints { get; set; }

    // XP Level System
    public int CurrentLevel { get; set; } = 1;
    public int CurrentXP { get; set; } = 0;
    public int XpToNextLevel { get; set; } = 3;

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
            PlayerData data = GameData.LoadedPlayerData;
            if (data != null) {
                InitializeCharacterFromData(data);
            } else {
                InitializeCharacter();
            }
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

    void InitializeCharacterFromData(PlayerData data) {
        Character player = FindObjectOfType<Character>();
        player.CurrentLevel = data.currentLevel;
        player.CurrentHp = data.currentHp;
        player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

        if (!string.IsNullOrEmpty(data.equippedSpell)) {
            Debug.Log("Loading: " +  data.equippedSpell);
            player.CurrentSpell = Resources.Load<SpellSO>("Spells/" + data.equippedSpell);
        }

        List<ArtifactSO> loadedArtifacts = new List<ArtifactSO>();

        foreach (var artifactName in data.artifacts) {
            ArtifactSO artifact = Resources.Load<ArtifactSO>("Artifacts/" + artifactName);
            if (artifact != null) {
                loadedArtifacts.Add(artifact);
            } else {
                Debug.LogWarning($"Artifact {artifactName} not found in Resources!");
            }
        }
        player.SetArtifacts(loadedArtifacts);
    }

    public void GainXP(int xp) {
        CurrentXP += xp;

        if (CurrentXP >= XpToNextLevel) {
            CurrentXP -= XpToNextLevel;
            CurrentLevel++;
            LevelUp();

            XpToNextLevel = Mathf.RoundToInt(XpToNextLevel * 1.25f);
        }

        OnXpChanged?.Invoke(CurrentXP, XpToNextLevel);
    }

    private void LevelUp() {
        GameManager.Instance.TogglePause();
        OnLevelUp?.Invoke(CurrentLevel); // current level will be to apply weight in upgrade rarity
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
        foreach (ArtifactSO artifact in CurrentArtifacts) {
            artifact.artifactEffect.OnShoot(null, weapon, dice, projectile);
        }
    }

    public List<ArtifactSO> GetArtifacts() {
        return CurrentArtifacts.ToList();
    }

    public void SetArtifacts(List<ArtifactSO> artifacts) {
        CurrentArtifacts.Clear();
        CurrentArtifacts.AddRange(artifacts);
    }

    // SPELLS
    public void CastSpell() {
        if (CurrentSpell != null) {
            if (CurrentSpellPoints == CurrentSpell.cost) {
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
        if (CurrentHp <= 0) {
            Die();
        }
    }

    public void Die() {
        Debug.Log("Die");
        GameManager.Instance.GameOver();
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