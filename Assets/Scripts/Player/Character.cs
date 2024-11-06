using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour {
    public CharacterSO characterSO;

    private bool isGamePaused = false;

    // Base Stats

    private int currentHp;
    public int CurrentHp {
        get { return currentHp; }
        set { currentHp = value; }
    }

    private float currentReloadSpeed;
    public float CurrentReloadSpeed {
        get { return currentReloadSpeed; }
        set { currentReloadSpeed = value; }
    }

    private float currentFireRate;
    public float CurrentFireRate {
        get { return currentFireRate; }
        set { currentFireRate = value; }
    }

    private float currentMovementSpeed;
    public float CurrentMovementSpeed {
        get { return currentMovementSpeed; }
        set { currentMovementSpeed = value; }
    }

    private float currentAttackRange;
    public float CurrentAttackRange {
        get { return currentAttackRange; }
        set { currentAttackRange = value; }
    }

    private float currentCriticalChance = 0f;
    public float CurrentCriticalChance {
        get { return currentCriticalChance; }
        set { currentCriticalChance = value; }
    }

    private float currentXpCollectionRange = 5f;
    public float CurrentXpCollectionRange {
        get { return currentXpCollectionRange; }
        set { currentXpCollectionRange = value; }
    }

    // Weapon
    private Weapon weapon;
    // Artifacts
    private List<ArtifactSO> currentArtifacts = new List<ArtifactSO>();
    // Spell
    private SpellSO currentSpell;
    public SpellSO CurrentSpell {
        get { return currentSpell; }
    }

    private int currentSpellPoints;
    public int CurrentSpellPoints {
        get { return currentSpellPoints; }
        set { currentSpellPoints = value; }
    }

    // XP Level System
    private int currentLevel = 1;
    private int currentXP = 0;
    private int xpToNextLevel = 5;

    // Unity events
    public event Action<int, int> OnXpChanged; // currentXP, xpToNextLevel
    public event Action<int> OnLevelUp; // currenLevel
    public event Action<SpellSO, int> OnEquipSpell; // currentSpell, currentSpellPoints
    public event Action<int> OnIncreaseSpellPoints; // currentSpell, currentSpellPoints
    public event Action<ArtifactSO> OnEquipArtifact; // artifact

    // Getters and Setters
    public float MovementSpeed => currentMovementSpeed;

    void Awake() {
        if (characterSO == null) {
            Debug.LogError("Character SO not set");
        } else {
            InitializeCharacter();
        }
    }

    void Start() {
        OnEquipSpell?.Invoke(currentSpell, currentSpellPoints);
        OnEquipArtifact?.Invoke(currentArtifacts.First());

        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();

        weapon.OnReload += IncreaseSpellPoints;
    }

    public void Update() {
        if (isGamePaused) return;

        if (Input.GetMouseButtonDown(0)) {
            GainXP(1);
        }
    }

    void InitializeCharacter() {
        currentHp = characterSO.hp;
        currentReloadSpeed = characterSO.reloadSpeed;
        currentFireRate = characterSO.fireRate;
        currentMovementSpeed = characterSO.movementSpeed;
        currentAttackRange = characterSO.attackRange;

        currentArtifacts.Add(characterSO.initialArtifact);

        currentSpell = characterSO.initialSpell;

        if (currentSpell == null) {
            Debug.LogWarning("Initial spell is not set in CharacterSO");
        }

        currentSpellPoints = 0;
    }

    public void GainXP(int xp) {
        this.currentXP += xp;

        if(this.currentXP >= xpToNextLevel) {
            this.currentXP -= xpToNextLevel;
            this.currentLevel++;
            LevelUp();

            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.25f);

            OnLevelUp?.Invoke(currentLevel);
        }

        OnXpChanged?.Invoke(currentXP, xpToNextLevel);

    }

    private void LevelUp() {
        GameManager.Instance.TogglePause();
        OnLevelUp?.Invoke(currentLevel); // current level will be to apply weight in upgrade rarity
    }

    private void IncreaseSpellPoints(int value) {
        currentSpellPoints = Mathf.Clamp(currentSpellPoints + value, 0, currentSpell.cost);
        OnIncreaseSpellPoints?.Invoke(currentSpellPoints);
    }

    private void OnEnable() {
        GameManager.OnGamePaused += HandleGamePaused;
        GameManager.OnGameResumed += HandleGameResumed;
    }

    private void OnDisable() {
        GameManager.OnGamePaused -= HandleGamePaused;
        GameManager.OnGameResumed -= HandleGameResumed;
    }

    private void HandleGamePaused() {
        isGamePaused = true;
    }

    private void HandleGameResumed() {
        isGamePaused = false;
    }
}