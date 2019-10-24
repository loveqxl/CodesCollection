using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Week6
{
    [CreateAssetMenu(fileName = "New State", menuName = "Week6/AbilityData/Jump")]
    public class Jump : StateData
    {
        [Range(0f,1f)]
        public float JumpTiming;
        public float jumpForce;

        public float BlockDistance;
        

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (JumpTiming == 0f) {
                characterState.GetCharacterControl(animator).RIGID_BODY.AddForce(Vector3.up.normalized * jumpForce);
                control.animationProgress.Jumped = true;
            }
            
            animator.SetBool(TransitionParameter.Grounded.ToString(),false);
        }

        override public void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (!control.animationProgress.Jumped && stateInfo.normalizedTime >= JumpTiming) {
                characterState.GetCharacterControl(animator).RIGID_BODY.AddForce(Vector3.up.normalized * jumpForce);
                control.animationProgress.Jumped = true;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            control.animationProgress.Jumped = false;
            if (animator.GetBool(TransitionParameter.JumpForward.ToString())){
                animator.SetBool(TransitionParameter.JumpForward.ToString(), false);
            }
        }

    }
}


