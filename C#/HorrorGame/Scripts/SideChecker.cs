using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorrorGame
{
    public class SideChecker : MonoBehaviour
    {
        private Door mydoor;
        // Start is called before the first frame update
        void Start()
        {
            mydoor = GetComponentInParent<Door>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player") {
                mydoor.onRightSide = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                mydoor.onRightSide = false;
            }
        }
    }
}
