using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorrorGame {
    public class Buddah : InteractiveObject
    {
        public Item item;

        private AudioSource playerAudioSource;

        public AudioClip pickupSound;

        private void Start()
        {
            playerAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }

        public override void Interact()
        {
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
