using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellDisplay : MonoBehaviour {
    private GameObject playerGO;
    private Character player;
    [SerializeField]
    private Image currentSpell;
    [SerializeField]
    private TMP_Text currentPoints;
    [SerializeField]
    private TMP_Text cost;

    private void Start() {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) {
            player = playerObj.GetComponent<Character>();
            BindPlayerEvents();
        } else {
            Debug.LogError("Player not found in scene at Start.");
        }
    }

    private void OnEnable() {
        if (player != null) {
            BindPlayerEvents();
        }
    }

    private void BindPlayerEvents() {
        player.OnEquipSpell += UpdateSpellDisplay;
        player.OnIncreaseSpellPoints += UpdatePointsDisplay;

        UpdateSpellDisplay(player.CurrentSpell, player.CurrentSpellPoints);
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
