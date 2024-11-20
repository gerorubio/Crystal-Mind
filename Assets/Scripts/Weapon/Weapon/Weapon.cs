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
        set { fireRate = value; }
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

    private float attackRange = 10f;

    // Ammunition System
    public int[] initialAmmunition = new int[6];
    protected AmmunitionSystem ammunitionSystem;
    public AmmunitionSystem AmmunitionSystem {
        get { return ammunitionSystem; }
    }

    public float AttackRange { get => attackRange; set => attackRange = value; }

    // UI Why is this here?
    private DiceDisplay diceDisplay; // Reference to DiceDisplay script

    // GameObjets
    //  Projectile source
    protected GameObject projectileSource;
    // Parent projectiles
    protected GameObject parentProjectiles;

    // Events
    public event Action<int> OnReload; // Reload event
    public event Action<Weapon, Dice, Projectile> OnShoot; // Shoot event

    // METHODS
    protected virtual void Awake() {
        InitializeProjectileSource();
        InitializeAmmunitionSystem();
    }

    private void Start() {
        diceDisplay = GameObject.Find("Canvas").GetComponent<DiceDisplay>();
    }

    protected virtual void Update() {
        if (isGamePaused || isReloading) return;
    }

    private void InitializeProjectileSource() {
        projectileSource = transform.Find("ProjectileSource")?.gameObject;
        if (projectileSource == null) {
            Debug.LogError("ProjectileSource not found. Ensure the child object exists and is named 'ProjectileSource'.");
        }

        parentProjectiles = new GameObject("Bullet Container");
    }

    private void InitializeAmmunitionSystem() {
        ammunitionSystem = new AmmunitionSystem();
        int d4 = initialAmmunition[0];
        int d6 = initialAmmunition[1];
        int d8 = initialAmmunition[2];
        int d10 = initialAmmunition[3];
        int d12 = initialAmmunition[4];
        int d20 = initialAmmunition[5];
        ammunitionSystem.InitializeAmmunition(d4, d6, d8, d12, d10, d20);
    }

    private void ShootHandler() {
        if (isGamePaused || isReloading) {
            return;
        }

        if (ammunitionSystem.CurrentAmmunition.Count > 0) {
            if (Time.time >= nextFiretime) {
                Shoot();
                nextFiretime = Time.time + fireRate;
            }
        } else if(!isReloading) {
            StartCoroutine(ReloadRoutine());
        }
        UpdateDisplay();
    }

    protected abstract void Shoot(); // Abstract method for shooting, implemented in derived classes

    protected void TriggerOnShoot(Dice dice, Projectile projectile) {
        OnShoot?.Invoke(this, dice, projectile);
    }

    protected void ReloadHandler() {
        OnReload?.Invoke(ammunitionSystem.RemainingAmmunitionValue);
        StartCoroutine(ReloadRoutine());
    }

    protected virtual IEnumerator ReloadRoutine() {
        isReloading = true;
        diceDisplay.StartRotatingDice();
        ammunitionSystem.Reload();

        yield return new WaitForSeconds(reloadTime);

        isReloading = false;
        diceDisplay.StopRotatingDice();
        UpdateDisplay();
    }

    protected virtual void UpdateDisplay() {
        diceDisplay.UpdateDiceDisplay(ammunitionSystem.CurrentAmmunition);
    }

    private void OnEnable() {
        GameManager.OnGamePaused += HandleGamePaused;
        GameManager.OnGameResumed += HandleGameResumed;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null) {
            playerController.OnShoot += ShootHandler;
            playerController.OnReload += ReloadHandler;
        }
    }

    private void OnDisable() {
        GameManager.OnGamePaused -= HandleGamePaused;
        GameManager.OnGameResumed -= HandleGameResumed;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if(playerController != null) {
            playerController.OnShoot -= ShootHandler;
            playerController.OnReload += ReloadHandler;
        }
    }

    private void HandleGamePaused() {
        isGamePaused = true;
    }

    private void HandleGameResumed() {
        isGamePaused = false;
    }
}
