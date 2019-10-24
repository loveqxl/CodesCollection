using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{
    [CreateAssetMenu(fileName = "New State", menuName = "Week6/AbilityData/ResetLocalPosition")]
    public class ResetLocalPosition : StateData
    {
        public bool onStart;
        public bool onEnd;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (onStart) {
                CharacterControl control = characterState.GetCharacterControl(animator);
                control.SkinnedMeshAnimator.transform.localPosition = Vector3.zero;
                control.SkinnedMeshAnimator.transform.localRotation = Quaternion.identity;
            }
        }

        override public void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (onEnd)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                control.SkinnedMeshAnimator.transform.localPosition = Vector3.zero;
                control.SkinnedMeshAnimator.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
