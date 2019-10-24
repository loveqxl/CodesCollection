using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Week6 {
    public class PlayGame : MonoBehaviour
    {
        public GameObject loadingScreen;
        public Slider slider;
        public Text laodingText;
        public CharacterSelect characterSelect;
        public Button playButton;
        public Text playButtonText;
        public Animator blackscreenAnimator;
        AsyncOperation operation;

        private void Start()
        {
            loadingScreen.SetActive(false);
            operation = null;
        }

        private void Update()
        {

            if (characterSelect.SelectedCharacterType == PlayableCharacterType.NONE) {
                playButton.GetComponent<Button>().interactable = false;
                playButtonText.text = "Please Select a Character!";
            } else if (characterSelect.SelectedCharacterType == PlayableCharacterType.lola) {
                playButton.GetComponent<Button>().interactable = true;
                playButtonText.text = "PLAY WITH LOLA";
            }
            else if (characterSelect.SelectedCharacterType == PlayableCharacterType.dummy)
            {
                playButton.GetComponent<Button>().interactable = true;
                playButtonText.text = "PLAY WITH DUMMY";
            }
        }

        public void PlayWeek6() {

                if (characterSelect.SelectedCharacterType != PlayableCharacterType.NONE)
                {
                    StartCoroutine(LoadAsynchronously());
                }
                else
                {
                playButton.GetComponent<Button>().interactable = false;
                playButtonText.text = "Please Select a Character!";
                Debug.Log("Must Select character");
                }
        }

        IEnumerator LoadAsynchronously() {
            operation = SceneManager.LoadSceneAsync("Main");
            loadingScreen.SetActive(true);
            operation.allowSceneActivation = false;
            while (operation.progress<0.89) {
                float progress = Mathf.Clamp01(operation.progress / 0.89f);
                slider.value = progress;
                laodingText.text = "LOADING..." + (int)(progress*100f)+"%";
                //Debug.Log("laoding");
                yield return null;
            }
            FadeInScene();
        }

        

        public void FadeInScene() {
            blackscreenAnimator.SetTrigger("FadeOut");
        }

        public void GoToNextScene() {
            if (operation != null)
            {
                operation.allowSceneActivation = true;
            }
        }

        public void QuitGame() {
            Application.Quit();
        }

    }
}

