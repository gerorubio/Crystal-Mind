using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellDisplay : MonoBehaviour {
    private Character player;
    public Image currentSpell;
    public TMP_Text currentPoints;
    public TMP_Text cost;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

        if (player == null) {
            Debug.LogError("Player not found in scene");
        } else {
            player.OnEquipSpell += UpdateSpellDisplay;
            player.OnIncreaseSpellPoints += UpdatePointsDisplay;
        }
    }

    // New spell equipped
    void UpdateSpellDisplay(SpellSO spell, int points) {
        currentSpell.sprite = spell.artWork;
        currentPoints.text = points.ToString();
        cost.text = spell.cost.ToString();
    }

    // Update only current points
    void UpdatePointsDisplay(int points) {
        currentPoints.text = points.ToString();
    }
}
