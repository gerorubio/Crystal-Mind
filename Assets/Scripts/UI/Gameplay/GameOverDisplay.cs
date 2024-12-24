using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverDisplay : MonoBehaviour {
    [SerializeField]
    private GameObject gameOverScreen;

    private void OnEnable() {
        GameManager.OnGameOver += DisplayGameOver;
    }

    private void OnDisable() {
        GameManager.OnGameOver -= DisplayGameOver;
    }

    private void DisplayGameOver() {
        if (gameOverScreen != null) {
            gameOverScreen.SetActive(true);
        }
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadScene("StartMenu");
    }
}
