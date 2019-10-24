using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorrorGame
{
    public class JumpScareLight : JumpScareObject
    {

        public override void TriggerJumpScares()
        {
            GetComponent<Animator>().SetTrigger("Close");
            AudioSource audio = GetComponent<AudioSource>();
            if (audio != null) {
                audio.Play();
            }
        }
    }
}
