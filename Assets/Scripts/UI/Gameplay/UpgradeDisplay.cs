using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDisplay : MonoBehaviour {
    public GameObject upgradeUI;

    void OnEnable() {
        Character character = FindObjectOfType<Character>();
        character.OnLevelUp += ShowUpgradeUI;
    }

    void OnDisable() {
        Character character = FindObjectOfType<Character>();
        character.OnLevelUp -= ShowUpgradeUI;
    }

    private void ShowUpgradeUI(int level) {
        if(upgradeUI != null) {
            upgradeUI.SetActive(true);
        }
    }
}
