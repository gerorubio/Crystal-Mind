using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public CharacterSO characterSO;

    private int currentHp;
    private float currentReloadSpeed;
    private float currentFireRate;
    private float currentMovementSpeed;
    private float currentAttackRange;

    private int currentLevel = 1;
    private int currentXP = 0;
    private int xpToNextLevel = 5;

    // Unity events
    public event Action<int, int> OnXpChanged; // currentXP, xpToNextLevel
    public event Action<int> OnLevelUp; // currenLevel

    // Getters and Setters
    public float MovementSpeed => currentMovementSpeed;

    void Start() {
        if (characterSO == null) {
            Debug.LogError("Character SO not set");
        } else {
            InitializeCharacter();
        }
    }

    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            GainXP(1);
        }
    }

    void InitializeCharacter() {
        this.currentHp = characterSO.hp;
        this.currentReloadSpeed = characterSO.reloadSpeed;
        this.currentFireRate = characterSO.fireRate;
        this.currentMovementSpeed = characterSO.movementSpeed;
        this.currentAttackRange = characterSO.attackRange;
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
}
