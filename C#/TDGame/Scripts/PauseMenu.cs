using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public SceneFader sceneFader;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
            Toggle();
        }
    }

    public void Toggle() {
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }
    }

    public void Retry() {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu() {
        Toggle();
        sceneFader.FadeTo("MainMenu");
    }
}
