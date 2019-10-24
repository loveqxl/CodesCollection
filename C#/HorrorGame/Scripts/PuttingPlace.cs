using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace HorrorGame {
    public class PuttingPlace : InteractiveObject
    {
        public Transform puttingSlot;
        public GameObject useItemUI;
        private FirstPersonController firstPersonController;
        public AudioSource audioSource;
        public AudioClip puttingSound;
        public string answerObjectName;

        private void Start()
        {
            firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
            audioSource = GetComponent<AudioSource>();
        }

        public override void Interact()
        {
            OpenUseItemUI();
        }


        private void OpenUseItemUI() {
            useItemUI.SetActive(true);
            useItemUI.GetComponent<UseItemUI>().currentPuttingPlace = this;
            useItemUI.GetComponent<UseItemUI>().descriptionText.text = "";
            Time.timeScale = 0;
            firstPersonController.enabled = false;
            firstPersonController.transform.GetChild(0).GetComponentInChildren<LightSwitch>().enabled = false;
            firstPersonController.m_MouseLook.SetCursorLock(false);
        }

        public bool? GetAnswer() {
            int objNum = transform.GetChild(0).childCount;
            if (objNum == 0)
            {
                return null;
            }
            else {
                string playerAnswer = transform.GetChild(0).GetChild(0).name;
                if (playerAnswer == answerObjectName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
           
        }
    }
}
