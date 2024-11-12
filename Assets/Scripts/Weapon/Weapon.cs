using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float reloadTime = 2f;
    private bool isReloading = false;
    private bool isGamePaused = false;

    private List<Dice> ammunition = new List<Dice>();
    public List<Dice> Ammunition { get { return ammunition; } }

    private List<Dice> currentAmmunition = new List<Dice>();

    public DiceDisplay diceDisplay; // Reference to DiceDisplay script

    private bool piercing = false;
    public bool Piercing {
        get { return piercing; }
        set { piercing = value; }
    }

    private float projectileSize;
    public float ProjectileSize {
        get { return projectileSize; }
        set { projectileSize = value; }
    }

    private float knockback;
    public float Knockback {
        get { return knockback; }
        set { knockback = value; }
    }

    // Events
    public event Action<int> OnReload; // reload

    void Awake() {

        ammunition.Add(new Dice(4));
        ammunition.Add(new Dice(6));
        ammunition.Add(new Dice(8));
        ammunition.Add(new Dice(10));
        ammunition.Add(new Dice(12));
        ammunition.Add(new Dice(20));


        Reload();
        UpdateDisplay();
    }

    void Update() {
        if (isGamePaused) return;

        if (Input.GetMouseButtonDown(0)) {
            Shoot();
            UpdateDisplay();
        }

        if (Input.GetMouseButtonDown(1)) {
            StartCoroutine(ReloadRoutine());
        }
    }

    void Shoot() {
        if (currentAmmunition.Count > 0 && !isReloading) {
            // Roll dice and get the result face
            Face face = currentAmmunition[0].currentFace;
            // Remove dice from ammo
            currentAmmunition.RemoveAt(0);

            //Debug.Log("Value: " + face.value + ", Effect: " + face.effect);
        } else {
            StartCoroutine(ReloadRoutine());
            //Debug.Log("Reloading...");
        }
    }

    private IEnumerator ReloadRoutine() {
        isReloading = true;
        
        diceDisplay.StartRotatingDice();
        Reload();

        yield return new WaitForSeconds(reloadTime);

        isReloading = false;

        diceDisplay.StopRotatingDice();

        UpdateDisplay();

        
    }

    void Reload() {
        foreach (Dice dice in ammunition) {
            dice.PrintAllFaces();
        }
        // For spell increase count
        int remainingAmmunition = 0;

        for (int i = 0; i < currentAmmunition.Count; i++) {
            remainingAmmunition += currentAmmunition[i].currentFace.value;
        }

        OnReload?.Invoke(remainingAmmunition);

        currentAmmunition = Shuffle();

        for (int i = 0; i < currentAmmunition.Count; i++) {
            currentAmmunition[i].RollDice();
        }
    }

    // Fisher–Yates shuffle for Lists
    public List<Dice> Shuffle() {
        List<Dice> shuffleAmmunition = new List<Dice>(this.ammunition);

        int n = shuffleAmmunition.Count;

        while (n > 1) {
            int k = UnityEngine.Random.Range(0, n--);
            Dice temp = shuffleAmmunition[n];
            shuffleAmmunition[n] = shuffleAmmunition[k];
            shuffleAmmunition[k] = temp;
        }

        return shuffleAmmunition;
    }

    void UpdateDisplay() {
        diceDisplay.UpdateDiceDisplay(this.currentAmmunition);
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
