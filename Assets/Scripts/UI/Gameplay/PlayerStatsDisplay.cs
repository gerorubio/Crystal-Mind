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

    private void Start() {
        player = FindObjectOfType<Character>();

        hp.text = "HP: " + player.CurrentHp;
        fireRate.text = "Fire rate: " + player.CurrentFireRate;
        movementSpeed.text = "Movement speed: " + player.CurrentMovementSpeed;
        reloadSpeed.text = "Reload speed: " + player.CurrentReloadSpeed;
        attackRange.text = "Attack range: " + player.CurrentAttackRange;
        criticalChance.text = "Critical chance: " + player.CurrentCriticalChance;
    }
}