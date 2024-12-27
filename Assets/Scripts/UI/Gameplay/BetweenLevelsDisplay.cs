using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BetweenLevelsDisplay : MonoBehaviour {
    public GameObject betweenLevelUI;
    public EnemySpawn spawn;

    public void SaveGame() {
        Character player = GameObject.FindWithTag("Player").GetComponent<Character>();

        if (player != null) {
            SaveSystem.SaveData(player, spawn);
            SceneManager.LoadScene("StartMenu");
        } else {
            Debug.LogError("Not player found");
        }
    }

    public void Resume() {
        GameManager.Instance.ResumeGame();
        if (betweenLevelUI != null) {
            betweenLevelUI.SetActive(false);
        }
    }
}
