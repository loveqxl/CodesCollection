using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorrorGame
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        public bool interactive = true;
        public abstract void Interact();

        public virtual void PickUp() {

        }
    }
}
