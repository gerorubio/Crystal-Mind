using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    // Game state
    protected bool isGamePaused = false;
    protected bool isReloading = false;

    // Stats
    public float ReloadTime { get; set; } = 2f;
    public float FireRate { get; set; } = 0.5f;
    public float NextFireTime { get; set; } = 0f;
    public bool Piercing { get; set; } = false;
    public float ProjectileSize { get; set; }
    public float Knockback { get; set; }
    public float AttackRange { get; set; } = 10f;


    protected float nextFiretime = 0f;

    // Ammunition System
    public int[] initialAmmunition = new int[6];
    protected AmmunitionSystem ammunitionSystem;
    public AmmunitionSystem AmmunitionSystem {
        get { return ammunitionSystem; }
    }

    // UI Why is this here?
    private DiceDisplay diceDisplay; // Reference to DiceDisplay script

    // GameObjets
    //  Projectile source
    protected GameObject projectileSource;
    // Parent projectiles
    protected GameObject parentProjectiles;

    // Audio Source
    protected AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>();
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
                nextFiretime = Time.time + FireRate;
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
        if(!isReloading) {
            OnReload?.Invoke(ammunitionSystem.RemainingAmmunitionValue);
            StartCoroutine(ReloadRoutine());
        }
    }

    protected virtual IEnumerator ReloadRoutine() {
        isReloading = true;
        diceDisplay.StartRotatingDice();
        ammunitionSystem.Reload();

        // Audio
        DicesSFXHandler.Instance?.ShuffleDice();

        yield return new WaitForSeconds(ReloadTime);

        // Audio
        DicesSFXHandler.Instance?.StopShuffleDice();
        DicesSFXHandler.Instance?.RollDice(ammunitionSystem.Ammunition);

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
