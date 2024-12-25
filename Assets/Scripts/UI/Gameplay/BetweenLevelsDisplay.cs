using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BetweenLevelsDisplay : MonoBehaviour {
    public GameObject betweenLevelUI;

    public void SavePlayer() {
        Character player = GameObject.FindWithTag("Player").GetComponent<Character>();

        if (player != null) {
            SaveSystem.SavePlayer(player);
            SceneManager.LoadScene("StartMenu");
        } else {
            Debug.LogError("Not player found");
        }
    }

    public void ResumeFromBetweenLevels() {
        GameManager.Instance.ResumeGame();
        if (betweenLevelUI != null) {
            betweenLevelUI.SetActive(false);
        }
    }
}
