using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public static bool GameIsPaused { get; private set; } = false;

    public static event Action OnGamePaused;
    public static event Action OnGameResumed;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void TogglePause() {
        if (GameIsPaused) {
            ResumeGame();
        } else {
            PauseGame();
        }
    }

    public void PauseGame() {
        if (GameIsPaused) return;

        GameIsPaused = true;
        Time.timeScale = 0f;
        OnGamePaused?.Invoke();
    }

    public void ResumeGame() {
        if (!GameIsPaused) return;

        GameIsPaused = false;
        Time.timeScale = 1f;
        OnGameResumed?.Invoke();
    }
}
