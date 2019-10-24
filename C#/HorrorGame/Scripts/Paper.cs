using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace HorrorGame
{
    public class Paper : InteractiveObject
    {
        public PaperItem item;

        public GameObject paperUI;

        public JumpScareObject[] jumpScareObjs;

        private FirstPersonController firstPersonController;

        private AudioSource playerAudioSource;

        public AudioClip pickupSound;

        private void Start()
        {
            firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
            playerAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
            item.canBeRemoved = false;
        }

        public override void Interact()
        {
            item.firstPersonController = firstPersonController;
            item.paperUI = paperUI;
            item.jumpScareObjs = jumpScareObjs;
            PickUp();
        }


        public override void PickUp()
        {
            bool wasPickUp = Inventory.Instance.Add(item);

            if (wasPickUp)
            {
                MessageManager.Instance.ShowMessage("You Picked Up " + item.name);
                playerAudioSource.clip = pickupSound;
                playerAudioSource.Play();
                Destroy(gameObject);
            }
        }

    }
}
