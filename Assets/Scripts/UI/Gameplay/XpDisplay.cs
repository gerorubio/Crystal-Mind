using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XpDisplay : MonoBehaviour {

    private Character character;

    public Image levelProgression;
    public TMP_Text level;

    void Start() {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

        if (character == null) {
            Debug.LogError("Player not found in scene");
        } else {
            character.OnXpChanged += UpdateXP;
            character.OnLevelUp += UpdateLevel;
        }
    }

    void UpdateXP(int currentXp, int xpToNextLevel) {
        levelProgression.fillAmount = (float) currentXp / xpToNextLevel;
    }
    void UpdateLevel(int currentLevel) {
        level.text = "Level " + currentLevel;
    }
}
