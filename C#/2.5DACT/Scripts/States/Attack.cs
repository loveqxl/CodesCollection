using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Week6
{
    [CreateAssetMenu(fileName = "New State", menuName = "Week6/AbilityData/Attack")]
    public class Attack : StateData
    {
        public bool debug;
        public float StartAttackTime;
        public float EndAttackTime;
        public List<string> ColliderNames = new List<string>();
        public bool LaunchIntoAir;
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;
        public int damageAmount;
        public AudioClip hitSound;
        private List<AttackInfo> FinishedAttacks = new List<AttackInfo>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Attack.ToString(), false);
            characterState.GetCharacterControl(animator).Attack_Normal = false;
            GameObject obj = PoolManager.Instance.GetObject(PoolObjectType.ATTACKINFO);
            AttackInfo info = obj.GetComponent<AttackInfo>();
            obj.SetActive(true);
            info.ResetInfo(this,characterState.GetCharacterControl(animator));

            if (!AttackManager.Instance.CurrentAttacks.Contains(info)) {
                AttackManager.Instance.CurrentAttacks.Add(info);
            }
        }

        override public void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RegisterAttack(characterState, animator, stateInfo);
            DeregisterAttack(characterState, animator, stateInfo);
            CheckCombo(characterState, animator, stateInfo);
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

            ClearAttack();
        }

        public void RegisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) {
            if (StartAttackTime <= stateInfo.normalizedTime && EndAttackTime > stateInfo.normalizedTime) {
                foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks) {
                    if (info == null) {
                        continue;
                    }
                    if (!info.isRegistered && info.attackAbility == this) {
                        info.Register(this);

                    }
                }
            }
        }

        public void DeregisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) {
            if (stateInfo.normalizedTime >= EndAttackTime|| animator.GetBool(TransitionParameter.Attack.ToString())|| animator.GetBool(SpellTypeParameters.Hadoken.ToString())) {

                foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks) {
                    if (info == null) {
                        continue;
                    }
                    if (info.attackAbility == this && !info.isFinishied) {
                        info.isFinishied = true;
                        info.GetComponent<PoolObject>().TurnOff();
                    }
                }
            }
        }

        public void ClearAttack() {

            FinishedAttacks.Clear();
            foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks) {
                if (info == null || info.attackAbility==this) {
                    FinishedAttacks.Add(info);
                }
            }

            foreach (AttackInfo info in FinishedAttacks) {
                if (AttackManager.Instance.CurrentAttacks.Contains(info)) {
                    AttackManager.Instance.CurrentAttacks.Remove(info);
                }
            }

        }

        public void CheckCombo(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (stateInfo.normalizedTime >= StartAttackTime + (EndAttackTime - StartAttackTime) / 1.5f) {
                if (stateInfo.normalizedTime < EndAttackTime + (EndAttackTime - StartAttackTime) / 3f) {
                    if (control.Attack_Normal) {

                        animator.SetBool(TransitionParameter.Attack.ToString(),true);
                    }
                }
            }

            if (stateInfo.normalizedTime > StartAttackTime + (EndAttackTime - StartAttackTime) / 5f && stateInfo.normalizedTime < EndAttackTime+(EndAttackTime - StartAttackTime) / 5f)
            {
                if (control.Spell)
                {
                    animator.SetBool(SpellTypeParameters.Hadoken.ToString(), true);
                }
                else {
                    animator.SetBool(SpellTypeParameters.Hadoken.ToString(), false);
                }
            }
        }
    }
}
