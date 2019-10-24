using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorrorGame {
    public class Key : InteractiveObject
    {

        public KeyItem keyItem;

        public Door myDoor;

        private AudioSource playerAudioSource;

        public AudioClip pickupSound;

        public void Start()
        {
            playerAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
            keyItem.myDoor = myDoor;
            keyItem.used = false;
            keyItem.canBeRemoved = false;
            
        }

        public override void Interact()
        {
            PickUp();
        }

        public override void PickUp() {
           
           bool wasPickUp = Inventory.Instance.Add(keyItem);

            if (wasPickUp) {
                MessageManager.Instance.ShowMessage("You Picked Up "+keyItem.name);
                playerAudioSource.clip = pickupSound;
                playerAudioSource.Play();
                Destroy(gameObject);
            }
        }

    }
}
