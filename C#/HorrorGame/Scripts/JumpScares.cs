using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorrorGame { 
public class JumpScares : MonoBehaviour
{
        public JumpScareObject[] jumpScareObjects;
        public bool oneOff = true;
        private bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
            triggered = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player") {
                if (!triggered) {
                    foreach (JumpScareObject obj in jumpScareObjects)
                    {
                        obj.TriggerJumpScares();
                        if (oneOff)
                        {
                            triggered = true;
                        }
                    }
                }
            }
        }

    }
}
