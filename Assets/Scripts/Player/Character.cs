using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour {
    public CharacterSO characterSO;

    private int currentHp;
    private float currentReloadSpeed;
    private float currentFireRate;
    private float currentMovementSpeed;
    private float currentAttackRange;
    //private float xpCollectionRange = 5f;

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
        if (Input.GetMouseButtonDown(0)) {

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

    void GainXP(int xp) {
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
        // Choose between spells, artifacts or dices
    }

    private void IncreaseSpellPoints(int value) {
        currentSpellPoints = Mathf.Clamp(currentSpellPoints + value, 0, currentSpell.cost);
        OnIncreaseSpellPoints?.Invoke(currentSpellPoints);
    }
}