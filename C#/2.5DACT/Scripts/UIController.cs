using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Week6 {
    public class UIController : MonoBehaviour
    {
        public CharacterControl player;
        public Image playerIcon;
        public Image playerHealthBar;

        public GameObject EnemyPanel;
        public Image EnemyIcon;
        public Image EnemyHealthBar;

        public GameObject MenuPanel;
        public BlackScreenMain BlackScreen;

        // Start is called before the first frame update
        void Start() {
            player = CharacterManager.Instance.GetPlayer(); 
            playerIcon.sprite = player.icon;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                MenuPanel.SetActive(!MenuPanel.activeSelf);
                if (MenuPanel.activeSelf)
                {
                    Time.timeScale = 0;
                }
                else {
                    Time.timeScale = 1;
                }
            }

            playerHealthBar.rectTransform.localScale =new Vector3(player.GetHPPct(), 1f,1f);
            playerHealthBar.color = new Color(Mathf.Clamp01((1-player.GetHPPct())*2), Mathf.Clamp01(player.GetHPPct()*2), playerHealthBar.color.b);
        }

        public void UpdateEnemyUI(CharacterControl enemy) {
            StopCoroutine(HideEnemyPanel());

            EnemyPanel.SetActive(true);
            EnemyIcon.sprite = enemy.icon;
            EnemyHealthBar.rectTransform.localScale = new Vector3(enemy.GetHPPct(), 1f, 1f);
            EnemyHealthBar.color = new Color(Mathf.Clamp01((1- enemy.GetHPPct())*2), Mathf.Clamp01(enemy.GetHPPct() * 2), EnemyHealthBar.color.b);


            //StartCoroutine(HideEnemyPanel());
        }


        public IEnumerator HideEnemyPanel() {
            yield return new WaitForSeconds(5);
            EnemyPanel.SetActive(false);
        }

        public void Continue()
        {
            Time.timeScale = 1;
            MenuPanel.SetActive(false);
        }

        public void GoToMainMenu() {
            Time.timeScale = 1;
            BlackScreenMain blackScreen = FindObjectOfType<BlackScreenMain>();
            blackScreen.sceneName = "SelectScene";
            blackScreen.GetComponent<Animator>().SetTrigger("FadeOut");
        }

        public void QuitGame() {
            Time.timeScale = 1;
            Application.Quit();
        }
    }

}

