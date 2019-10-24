using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorrorGame
{
    public class JumpScareSound : JumpScareObject
    {
        public AudioClip sound;
        private AudioSource audioSource;
        public Transform movingTarget;
        public float movingSpeed = 1f;
        private bool triggered = false;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public override void TriggerJumpScares()
        {
            audioSource.clip = sound;
            audioSource.Play();
            triggered = true;
        }

        private void Update()
        {
            if (triggered&&movingTarget!=null)
            {
                transform.Translate((movingTarget.position-transform.position).normalized * movingSpeed * Time.deltaTime);
            }
        }
    }
}
