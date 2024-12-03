using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenuUI;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.Instance.TogglePause();
            pauseMenuUI.SetActive(GameManager.GameIsPaused);
        }
    }
}