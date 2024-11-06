using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
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
