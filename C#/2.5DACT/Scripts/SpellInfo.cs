using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{
    public class SpellInfo : MonoBehaviour
    {
        public SpellTypeParameters spellType;
        public float dirWaitTime;
        public float atkWaitTime;
        //public List<TransitionParameter> inputsList;

        public GameObject bullet;
        public GameObject firepoint;
        public float firetime;
        public float flySpeed;
        public bool shot;

        public CharacterControl Attacker = null;
        public Spell spellAbility;
        public List<string> ColliderNames = new List<string>();
        public bool LaunchIntoAir;
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;
        public int CurrentHits;
        public bool isRegistered;
        public bool isFinishied;

        public void ResetInfo(Spell spell, CharacterControl attacker)
        {
            isRegistered = false;
            isFinishied = false;
            shot = false;
            firepoint = attacker.firePoint;
            spellAbility = spell;
            Attacker = attacker;
        }

        public void Register(Spell spell)
        {
            isRegistered = true;

            spellType = spell.spellType;
            spellAbility = spell;
            dirWaitTime = spell.dirWaitTime;
            atkWaitTime = spell.atkWaitTime;

            bullet = spell.bullet;
            firepoint = spell.firepoint;
            firetime = spell.firetime;
            flySpeed = spell.flySpeed;

            ColliderNames = spell.ColliderNames;
            LaunchIntoAir = spell.LaunchIntoAir;
            MustCollide = spell.MustCollide;
            MustFaceAttacker = spell.MustFaceAttacker;
            LethalRange = spell.LethalRange;
            MaxHits = spell.MaxHits;
            CurrentHits = 0;

        }

        private void OnDisable()
        {
            isFinishied = true;
        }
    }

}
