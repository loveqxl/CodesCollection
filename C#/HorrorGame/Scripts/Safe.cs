using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


namespace HorrorGame
{
    public class Safe : InteractiveObject
    {
        public bool open = false;
        public float safeOpenAngle = 120f;
        public float safeClosedAngle = 0f;
        public float smooth = 2f;
        public bool locked = true;
        public string password = "0000";
        public GameObject safeUI;
        public AudioSource audioSource;
        public AudioClip UnlockSound;
        public AudioClip openSound;
        private FirstPersonController firstPersonController;

        private void Start()
        {
            firstPersonController = FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>();
            audioSource = GetComponent<AudioSource>();
            open = false;
            locked = true;
        }

        private void Update()
        {
            if (open)
            {

                Quaternion targetRotationOpen = Quaternion.Euler(0, safeOpenAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationOpen, smooth * Time.deltaTime);
            }
            else
            {
                Quaternion targetRotationClose = Quaternion.Euler(0, safeClosedAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationClose, smooth * Time.deltaTime);
            }
        }

        public override void Interact()
        {
            ChangeSafeState();
        }

        private void ChangeSafeState() {
            if (!locked)
            {

                open = !open;
                audioSource.clip = openSound;
                audioSource.Play();
            }
            else
            {
                OpenSafeUI();
            }
        }

        private void OpenSafeUI() {
            safeUI.SetActive(true);
            safeUI.GetComponent<SafeUI>().currentSafe = this;
            Time.timeScale = 0;
            firstPersonController.enabled = false;
            firstPersonController.transform.GetChild(0).GetComponentInChildren<LightSwitch>().enabled = false;
            firstPersonController.m_MouseLook.SetCursorLock(false);
            
        }

        public void LockOpened() {
            if (locked) {
                locked = false;
                audioSource.clip = UnlockSound;
                audioSource.Play();
            }
        }
    }
}