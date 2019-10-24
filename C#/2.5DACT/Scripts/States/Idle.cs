using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Week6
{
    [CreateAssetMenu(fileName = "New State", menuName = "Week6/AbilityData/Idle")]
    public class Idle : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Jump.ToString(), false);
            animator.SetBool(TransitionParameter.Attack.ToString(), false);
            animator.SetBool(SpellTypeParameters.Hadoken.ToString(), false);
        }

        override public void UpdateAbility(CharacterState characterState, Animator animator,AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.MoveRight && control.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }

            if (control.Spell)
            {
                animator.SetBool(SpellTypeParameters.Hadoken.ToString(), true);

            }
            else {
                animator.SetBool(SpellTypeParameters.Hadoken.ToString(), false);
            }

            if (control.Attack_Normal) {
                animator.SetBool(TransitionParameter.Attack.ToString(), true);
            }

            if (control.Jump)
            {
                animator.SetBool(TransitionParameter.Jump.ToString(), true);
                if (control.JumpForward)
                {
                    animator.SetBool(TransitionParameter.JumpForward.ToString(), true);
                }
            }


            if (control.Crouch)
            {
                animator.SetBool(TransitionParameter.Crouch.ToString(), true);
            }
            else if (!control.Crouch) {
                animator.SetBool(TransitionParameter.Crouch.ToString(), false);
            }

            if (control.Dash)
            {
                animator.SetBool(SpellTypeParameters.Dash.ToString(), true);
            }
            else if (!control.Crouch)
            {
                animator.SetBool(SpellTypeParameters.Dash.ToString(), false);
            }

            if (control.MoveRight)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }

            if (control.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }
        }



        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}
