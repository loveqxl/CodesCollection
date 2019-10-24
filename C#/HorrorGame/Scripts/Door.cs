using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace HorrorGame {
    public class Door : InteractiveObject
    {
        public bool open=false;
        public bool vertical = false;
        public float doorOpenAngle = 90f;
        public float doorClosedAngle = 0f;

        public float smooth = 2f;

        public AudioClip openSound;
        public AudioClip lockSound;
        public AudioClip unlockSound;

        public AudioSource doorAudio;
        private int openDoorDir = 1;

        private Animator doorAnimator;

        public bool isLocked = false;
        public bool needKey = false;

        [HideInInspector]
        public bool onRightSide = false;

        public string message;
        public bool specialmessage=false;


        private void Start()
        {
            doorAudio = GetComponent<AudioSource>();
            doorAnimator = GetComponent<Animator>();
            doorAnimator.runtimeAnimatorController = null;
        }

        // Update is called once per frame
        void Update()
        {
            if (open)
            {

                Quaternion targetRotationOpen = Quaternion.Euler(0, doorOpenAngle*openDoorDir, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationOpen, smooth * Time.deltaTime);
            }
            else {
                Quaternion targetRotationClose = Quaternion.Euler(0, doorClosedAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationClose, smooth * Time.deltaTime);              
            }
        }

        public override void Interact() {
            ChangeDoorState();
        }

        public void ChangeDoorState() {

            if (!isLocked)
            {
                doorAnimator.runtimeAnimatorController = null;
                open = !open;
                doorAudio.clip = openSound;
                doorAudio.Play();
            }
            else {

                    doorAudio.clip = lockSound;
                if (needKey)
                {

                    List<Item> keys = new List<Item>();
                    for (int i = 0; i < Inventory.Instance.items.Count; i++)
                    {
                        if (Inventory.Instance.items[i] is KeyItem)
                        {
                            keys.Add(Inventory.Instance.items[i]);
                        }
                    }

                    foreach (KeyItem key in keys)
                    {
                        if (key.myDoor == this)
                        {
                            key.UnlockDoor();
                            doorAudio.clip = unlockSound;
                        }
                    }

                    if (isLocked)
                    {
                        doorAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Door");
                        message = "Need a key to open the door";
                        MessageManager.Instance.ShowMessage(message);
                    }
                }
                else if (!onRightSide) {
                    doorAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Door");
                    if (!specialmessage)
                    {
                        message = "Can't be opened at this side";
                    }
                    else {
                        message = "The door is locked";
                    }
                    MessageManager.Instance.ShowMessage(message);
                } 
                else if(!needKey&&onRightSide)
                {
                    isLocked = false;
                    doorAudio.clip = unlockSound;
                }

                doorAudio.Play();
            }

            if (open)
            {
                Transform player = FindObjectOfType<FirstPersonController>().transform;
                if (!vertical)
                {
                    if ((transform.position - player.position).normalized.z > 0)
                    {
                        openDoorDir = -1;
                    }
                    else
                    {
                        openDoorDir = 1;
                    }
                }
                else {
                    if ((transform.position - player.position).normalized.x > 0)
                    {
                        openDoorDir = 1;
                    }
                    else
                    {
                        openDoorDir = -1;
                    }
                }
            }
        }

    }
}

