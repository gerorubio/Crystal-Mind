using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    // Game state
    protected bool isGamePaused = false;
    protected bool isReloading = false;

    // Stats
    protected float reloadTime = 2f;
    public float ReloadTime {
        get { return reloadTime; }
        set { reloadTime = value; }
    }

    protected float fireRate = 0.5f;
    public float FireRate {
        get { return fireRate; }
        set {  fireRate = value; }
    }

    protected float nextFiretime = 0f;

    protected bool piercing = false;
    public bool Piercing {
        get { return piercing; }
        set { piercing = value; }
    }

    protected float projectileSize;
    public float ProjectileSize {
        get { return projectileSize; }
        set { projectileSize = value; }
    }

    protected float knockback;
    public float Knockback {
        get { return knockback; }
        set { knockback = value; }
    }

    // Ammunition System
    public int[] initialAmmunition = new int[6];
    protected AmmunitionSystem ammunitionSystem;
    public AmmunitionSystem AmmunitionSystem {
        get { return ammunitionSystem; }
    }

    // UI Why is this here?
    public DiceDisplay diceDisplay; // Reference to DiceDisplay script

    // GameObjets
    //  Projectile source
    protected GameObject projectileSource;
    // Parent projectiles
    protected GameObject parentProjectiles;

    // Events
    public event Action<int> OnReload; // Reload event

    // METHODS
    protected virtual void Awake() {
        InitializeProjectileSource();
        InitializeAmmunitionSystem();
    }

    protected virtual void Update() {
        if (isGamePaused || isReloading) return;
    }

    private void InitializeProjectileSource() {
        projectileSource = transform.Find("ProjectileSource")?.gameObject;
        if (projectileSource == null) {
            Debug.LogError("ProjectileSource not found. Ensure the child object exists and is named 'ProjectileSource'.");
        }

        parentProjectiles = new GameObject();
    }

    private void InitializeAmmunitionSystem() {
        ammunitionSystem =  new AmmunitionSystem();
        int d4 = initialAmmunition[0];
        int d6 = initialAmmunition[1];
        int d8 = initialAmmunition[2];
        int d10 = initialAmmunition[3];
        int d12 = initialAmmunition[4];
        int d20 = initialAmmunition[5];
        ammunitionSystem.InitializeAmmunition(d4, d6, d8, d12, d10, d20);
    }


    protected abstract void Shoot(); // Abstract method for shooting, implemented in derived classes

    protected virtual IEnumerator ReloadRoutine() {
        isReloading = true;
        diceDisplay.StartRotatingDice();
        ammunitionSystem.Reload();

        yield return new WaitForSeconds(reloadTime);

        isReloading = false;
        diceDisplay.StopRotatingDice();
        UpdateDisplay();
    }

    

    protected virtual void CreateProjectile(Projectile projectile, Face face) {
        if (projectile != null) {
            projectile.damage = face.value;
            projectile.effect = face.effect;
        }
    }


    protected virtual void UpdateDisplay() {
        diceDisplay.UpdateDiceDisplay(ammunitionSystem.CurrentAmmunition);
    }

    private void OnEnable() {
        GameManager.OnGamePaused += HandleGamePaused;
        GameManager.OnGameResumed += HandleGameResumed;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null) {
            playerController.OnShoot += Shoot;
        }
    }

    private void OnDisable() {
        GameManager.OnGamePaused -= HandleGamePaused;
        GameManager.OnGameResumed -= HandleGameResumed;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if(playerController != null) {
                playerController.OnShoot -= Shoot;
                }
    }

    private void HandleGamePaused() {
        isGamePaused = true;
    }

    private void HandleGameResumed() {
        isGamePaused = false;
    }
}
