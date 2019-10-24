using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{

    [CreateAssetMenu(fileName = "New State", menuName = "Week6/AbilityData/ToggleBoxCollider")]
    public class ToggleBoxCollider : StateData
    {
        public bool On;
        public bool onStart;
        public bool onEnd;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (onStart)
            {
                CharacterControl control = CharacterManager.Instance.GetCharacter(animator);
                ToggleColiider(control);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (onEnd)
            {
                CharacterControl control = CharacterManager.Instance.GetCharacter(animator);
                ToggleColiider(control);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        private void ToggleColiider(CharacterControl control) {
            control.RIGID_BODY.velocity = Vector3.zero;
            control.GetComponent<BoxCollider>().enabled=On;
        }
    }

}