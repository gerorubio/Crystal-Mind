using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public GameObject characterSelectionMenu;
    public GameObject loadDataMenu;

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Play() {
        SaveSystem.data = SaveSystem.LoadPlayer();

        if(SaveSystem.data != null ) {
            loadDataMenu.SetActive(true);
        } else {
            characterSelectionMenu.SetActive(true);
            transform.gameObject.SetActive(false);
        }
    }

    public void NewGame() {
        characterSelectionMenu.SetActive(true);
        loadDataMenu.SetActive(false);
        SaveSystem.DeleteSaveFile();
    }

    public void LoadGame() {
        GameData.LoadedPlayerData = SaveSystem.data;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}