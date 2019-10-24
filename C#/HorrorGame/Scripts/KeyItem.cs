using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorrorGame
{
    [CreateAssetMenu(fileName = "New KeyItem", menuName = "HorrorGame/KeyItem")]
    public class KeyItem : Item
    {
        public Door myDoor;

        public bool used = false;


        public void UnlockDoor() {
            if (myDoor != null)
            {
                if (myDoor.isLocked && !used)
                {
                    myDoor.isLocked = false;
                    used = true;
                    MessageManager.Instance.ShowMessage("You used "+name+" open the "+myDoor.name);
                    canBeRemoved = true;
                }
            }
        }
    }
}