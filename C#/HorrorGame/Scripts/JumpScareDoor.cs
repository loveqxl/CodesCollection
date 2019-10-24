using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorrorGame
{
    public class JumpScareDoor : JumpScareObject
    {

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public override void TriggerJumpScares()
        {

                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("KnockDoor");

        }
    }
}