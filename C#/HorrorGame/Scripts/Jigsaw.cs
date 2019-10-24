using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace HorrorGame
{
    public class Jigsaw : InteractiveObject
    {
        public Sprite[] sprites;
        public Sprite notCompleteCoverSprite;
        public Sprite CompleteCoverSprite;
        public GameObject JigsawUI;
        public CreateImage createImage;
        private FirstPersonController firstPersonController;
        public AudioSource audioSource;
        public AudioClip openSound;
        public AudioClip successSound;

        [HideInInspector]
        public bool solved = false;

        public KeyItem rewardItem;
        public Door myDoor;

        private void Awake()
        {
            solved = false;
            GetComponent<MeshRenderer>().material.SetTexture("Texture2D_1066FD47", notCompleteCoverSprite.texture);
            
        }

        private void Start()
        {
            firstPersonController = FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>();
            audioSource = GetComponent<AudioSource>();
        }

        public override void Interact()
        {
            JigsawPuzzle();
        }

        private void JigsawPuzzle() {
            if (!solved)
            {
                audioSource.clip = openSound;
                audioSource.Play();
                createImage.currentJigsaw = this;
                createImage.sprites = sprites;
                createImage.rewardItem = rewardItem;
                createImage.myDoor = myDoor;
                createImage.InitImg();
                JigsawUI.SetActive(true);
                Time.timeScale = 0;
                firstPersonController.enabled = false;
                firstPersonController.transform.GetChild(0).GetComponentInChildren<LightSwitch>().enabled = false;
                firstPersonController.m_MouseLook.SetCursorLock(false);
            }

        }
    }
}
