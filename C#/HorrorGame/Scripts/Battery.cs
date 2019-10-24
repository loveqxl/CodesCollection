using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorrorGame
{
    public class Battery : InteractiveObject
    {

        public BatteryItem batteryItem;
 

        private AudioSource playerAudioSource;

        public AudioClip pickupSound;

        public void Start()
        {
            playerAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
            batteryItem.canBeRemoved = false;

        }

        public override void Interact()
        {
            PickUp();
        }

        public override void PickUp()
        {


            bool wasPickUp = Inventory.Instance.Add(batteryItem);

            if (wasPickUp)
            {
                int batteryCount = 0;
                foreach (Item item in Inventory.Instance.items)
                {
                    if (item is BatteryItem)
                    {
                        batteryCount += 1;
                    }
                }

                if (batteryCount == 2)
                {
                    Inventory.Instance.items.Remove(batteryItem);

                    foreach (Item item in Inventory.Instance.items)
                    {
                        if (item is BatteryItem)
                        {
                            item.Count += 1;
                        }
                    }
                }else {
                    foreach (Item item in Inventory.Instance.items)
                    {
                        if (item is BatteryItem)
                        {
                            item.Count = 1;
                        }
                    }
                }

                if (Inventory.Instance.onItemChangedCallBack != null)
                {
                    Inventory.Instance.onItemChangedCallBack.Invoke();
                }
                MessageManager.Instance.ShowMessage("You Picked Up " + batteryItem.name);
               
                    playerAudioSource.clip = pickupSound;
                    playerAudioSource.Play();
                    Destroy(gameObject);
                }

            }
        }

    }
