using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{

    [CreateAssetMenu(fileName = "New State", menuName = "Week6/AbilityData/TeleportOnEdge")]
    public class TeleportOnEdge : StateData
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
            Vector3 endPostion = control.edgeChecker.GrabbedEdge.transform.position + control.edgeChecker.GrabbedEdge.EndPosition;
            control.transform.position = endPostion;
            control.SkinnedMeshAnimator.transform.position = endPostion;
            control.SkinnedMeshAnimator.transform.parent = control.transform;
        }
    }

}