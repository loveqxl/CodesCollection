using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorrorGame
{
    public class TriggerDoor : JumpScareObject
    {
        private Door door;
        private void Start()
        {
            door = GetComponent<Door>();
        }

        public override void TriggerJumpScares()
        {
            if (door.isLocked) {
                door.doorAudio.clip = door.unlockSound;
                door.doorAudio.Play();
                door.isLocked = false;
                MessageManager.Instance.ShowMessage("Some one unlocked a door");
            }
        }

    }
}
