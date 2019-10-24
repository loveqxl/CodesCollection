using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6 {
    [CreateAssetMenu(fileName = "New State", menuName = "Week6/AbilityData/TurnOnRootMotion")]
    public class TurnOnRootMotion : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

            CharacterControl control = characterState.GetCharacterControl(animator);
            control.SkinnedMeshAnimator.applyRootMotion = true;

        }

        override public void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

            CharacterControl control = characterState.GetCharacterControl(animator);
            control.SkinnedMeshAnimator.applyRootMotion = false;
        }
    }
}
