using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Week6
{
    [CreateAssetMenu(fileName = "New State", menuName = "Week6/AbilityData/Crouch")]
    public class Crouch : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Jump.ToString(), false);
            animator.SetBool(TransitionParameter.Attack.ToString(), false);
        }

        override public void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = CharacterManager.Instance.GetCharacter(animator);

            if (!control.Crouch)
            {
                animator.SetBool(TransitionParameter.Crouch.ToString(), false);
            }else if (control.Crouch)
            {
                animator.SetBool(TransitionParameter.Crouch.ToString(), true);
            }

            if (control.Spell) {
                animator.SetBool(SpellTypeParameters.Hadoken.ToString(), true);
            }
        }





        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}