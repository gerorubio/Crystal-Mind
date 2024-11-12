using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsDisplay : MonoBehaviour {
    public TMP_Text hp;
    public TMP_Text fireRate;
    public TMP_Text movementSpeed;
    public TMP_Text reloadSpeed;
    public TMP_Text attackRange;
    public TMP_Text criticalChance;

    private Character player;

    private float time = 0f;

    private void Start() {
        player = FindObjectOfType<Character>();
        UpdateStats();
    }

    void Update() {
        if (time >= 5f) {
            UpdateStats();
            time = 0f;
        } else {
            time += Time.deltaTime;
        }
    }

    private void UpdateStats() {
        hp.text = "HP: " + player.CurrentHp;
        fireRate.text = "Fire rate: " + player.CurrentFireRate;
        movementSpeed.text = "Movement speed: " + player.CurrentMovementSpeed;
        reloadSpeed.text = "Reload speed: " + player.CurrentReloadSpeed;
        attackRange.text = "Attack range: " + player.CurrentAttackRange;
        criticalChance.text = "Critical chance: " + player.CurrentCriticalChance;
    }
}