using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{

    [CreateAssetMenu(fileName = "New State", menuName = "Week6/AbilityData/OffsetOnEdge")]
    public class OffsetOnEdge : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            control.RIGID_BODY.velocity = Vector3.zero;
            GameObject anim = control.SkinnedMeshAnimator.gameObject;
            anim.transform.parent = control.edgeChecker.GrabbedEdge.transform;
            anim.transform.localPosition = control.edgeChecker.GrabbedEdge.Offset;         
            control.RIGID_BODY.velocity = Vector3.zero;

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
           
        }
    }

}

