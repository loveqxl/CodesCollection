using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6 {
    public class AttackInfo : MonoBehaviour
    {
        public CharacterControl Attacker=null;
        public Attack attackAbility;
        public List<string> ColliderNames = new List<string>();
        public bool LaunchIntoAir;
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;
        public int CurrentHits;
        public bool isRegistered;
        public bool isFinishied;
        public int damageAmount;
        public AudioClip hitSound;

        public void ResetInfo(Attack attack,CharacterControl attacker) {
            isRegistered = false;
            isFinishied = false;
            attackAbility = attack;
            Attacker = attacker;
        }

        public void Register(Attack attack) {
            isRegistered = true;

            damageAmount = attack.damageAmount;
            attackAbility = attack;
            ColliderNames = attack.ColliderNames;
            LaunchIntoAir = attack.LaunchIntoAir;
            MustCollide = attack.MustCollide;
            MustFaceAttacker = attack.MustFaceAttacker;
            LethalRange = attack.LethalRange;
            MaxHits = attack.MaxHits;
            CurrentHits = 0;
            hitSound = attack.hitSound;

        }

        private void OnDisable()
        {
            isFinishied = true;
        }
    }

}