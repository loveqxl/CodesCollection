using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

namespace HorrorGame
{
    public class UseItemUI : MonoBehaviour
    {
        public PuttingPlace currentPuttingPlace;
        public FirstPersonController firstPersonController;
        public Text descriptionText;

        private void Start()
        {
            firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Cancel") && gameObject.activeSelf)
            {
                CloseUseItemUI();
            }
        }

        public void CloseUseItemUI() {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            firstPersonController.enabled = true;
            firstPersonController.transform.GetChild(0).GetComponentInChildren<LightSwitch>().enabled = true;
            firstPersonController.m_MouseLook.SetCursorLock(true);
        }
    }
}
