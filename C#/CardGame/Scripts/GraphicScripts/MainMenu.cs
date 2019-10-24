using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject instructionCanvas;
    public void Play() {
        SceneManager.LoadScene(1);
    }
    public void Credit() {
        SceneManager.LoadScene(2);
    }

    public void HowToPlay() {
        instructionCanvas.SetActive(true);
    }

    public void BackToMenu() {
        instructionCanvas.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
