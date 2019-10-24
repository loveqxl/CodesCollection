using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

namespace HorrorGame
{
    public class EndGameCheck : InteractiveObject
    {
        public PuttingPlace[] puttingPlaces;

        public GameObject winView;
        public GameObject loseView;

        public Transform startPoint;
        

        private FirstPersonController firstPersonController;
        // Start is called before the first frame update
        void Start()
        {
          puttingPlaces=GameObject.FindObjectsOfType<PuttingPlace>();
          firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        }

        public override void Interact()
        {
            CheckGameEnd();
        }


        public void CheckGameEnd() {

            if (puttingPlaces[0].GetAnswer() == null || puttingPlaces[1].GetAnswer() == null)
            {
                restart();
            }
            else if (puttingPlaces[0].GetAnswer() == false && puttingPlaces[1].GetAnswer() == false)
            {
                lose();
            }
            else if (puttingPlaces[0].GetAnswer() == true && puttingPlaces[1].GetAnswer() == true) {
                win();
            }

        }

        private void win() {
            
            winView.SetActive(true);
            firstPersonController.enabled = false;
            firstPersonController.transform.GetChild(0).GetComponentInChildren<LightSwitch>().enabled = false;
            firstPersonController.m_MouseLook.SetCursorLock(false);
            StartCoroutine("GoToCredit");
        }

        private void lose() {
            
            loseView.SetActive(true);
            firstPersonController.enabled = false;
            firstPersonController.transform.GetChild(0).GetComponentInChildren<LightSwitch>().enabled = false;
            firstPersonController.m_MouseLook.SetCursorLock(false);
            StartCoroutine("GoToCredit");
        }

        private void restart() {
            
            firstPersonController.transform.position = startPoint.position;
            firstPersonController.transform.rotation = startPoint.rotation;
            MessageManager.Instance.ShowMessage("What happened?");
        }

        IEnumerator GoToCredit() {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("Credit");
        }

    }

}
