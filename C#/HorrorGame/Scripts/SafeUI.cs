using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
namespace HorrorGame
{
    public class SafeUI : MonoBehaviour
    {
        public Safe currentSafe;

        public int[] passwordNumbers = new int[3];

        public Text[] NumText = new Text[3];

        private FirstPersonController firstPersonController;

        private string currentInput;

        private AudioSource audioSource;

        public AudioClip increaseSound;
        public AudioClip decreaseSound;

        private void Start()
        {
            firstPersonController = FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>();
            audioSource = GetComponent<AudioSource>();

            for (int i = 0; i < passwordNumbers.Length; i++)
            {
                passwordNumbers[i] = 0;
                NumText[i].text = "0" + passwordNumbers[i].ToString();
            }

            currentInput = "";
        }

        private void Update()
        {

            if (currentInput == currentSafe.password) {
                currentSafe.LockOpened();
                currentSafe = null;
                Time.timeScale = 1;
                firstPersonController.enabled = true;
                firstPersonController.transform.GetChild(0).GetComponentInChildren<LightSwitch>().enabled = true;
                firstPersonController.m_MouseLook.SetCursorLock(true);
                gameObject.SetActive(false);
            }

            if (Input.GetButtonDown("Cancel"))
            {
                currentSafe = null;
                Time.timeScale = 1;
                firstPersonController.enabled = true;
                firstPersonController.transform.GetChild(0).GetComponentInChildren<LightSwitch>().enabled = true;
                firstPersonController.m_MouseLook.SetCursorLock(true);
                gameObject.SetActive(false);
            }
        }

        void UpdateNumber(int numIndex) {
            if (passwordNumbers[numIndex] < 10)
            {
                NumText[numIndex].text = "0" + passwordNumbers[numIndex].ToString();
            }
            else if (passwordNumbers[numIndex] >= 10) {
                NumText[numIndex].text = passwordNumbers[numIndex].ToString();
            }
            currentInput = "";
            for (int i = 0; i < NumText.Length; i++)
            {
                
                currentInput = currentInput + NumText[i].text;
            }
        }

        public void Increase(int numIndex) {
            audioSource.clip = increaseSound;
            audioSource.Play();
            if (passwordNumbers[numIndex] != 12)
            {
                passwordNumbers[numIndex] += 1;
            }
            else if (passwordNumbers[numIndex] == 12) {
                passwordNumbers[numIndex] = 0;
            }
            UpdateNumber(numIndex);
        }

        public void Decrease(int numIndex)
        {
            audioSource.clip = decreaseSound;
            audioSource.Play();
            if (passwordNumbers[numIndex] != 0)
            {
                passwordNumbers[numIndex] -= 1;
            }
            else if (passwordNumbers[numIndex] == 0)
            {
                passwordNumbers[numIndex] = 12;
            }
            UpdateNumber(numIndex);
        }
    }
}
