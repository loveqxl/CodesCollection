using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{

    [CreateAssetMenu(fileName = "New State", menuName = "Week6/AbilityData/CancelGrabbingEdge")]
    public class CancelGrabbingEdge : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = CharacterManager.Instance.GetCharacter(animator);
            control.edgeChecker.GrabbedEdge = null;
        }
    }

}
