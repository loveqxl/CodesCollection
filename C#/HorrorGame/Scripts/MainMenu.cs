using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HorrorGame {
    public class MainMenu : MonoBehaviour
    {
        public void StartGame() {
            SceneManager.LoadScene("Main");
        }

        public void QuitGame() {
            Application.Quit();
        }
    }
}
