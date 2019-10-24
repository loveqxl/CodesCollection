using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6 {
    [CreateAssetMenu(fileName ="New State", menuName ="Week6/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {
        public bool Constant;
        public AnimationCurve SpeedGraph;
        public float speed=1f;
        public float BlockDistance;
        private bool Self;

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control=characterState.GetCharacterControl(animator);
            

            if (control.Jump) {
                animator.SetBool(TransitionParameter.Jump.ToString(), true);
                if (control.MoveRight||control.MoveLeft)
                {
                    animator.SetBool(TransitionParameter.JumpForward.ToString(), true);
                }
            }

            if (control.Crouch)
            {
                animator.SetBool(TransitionParameter.Crouch.ToString(), true);
            }
            else if (!control.Crouch)
            {
                animator.SetBool(TransitionParameter.Crouch.ToString(), false);
            }

            if (control.Dash&&!animator.GetBool("Jump"))
            {
                animator.SetBool(TransitionParameter.Dash.ToString(), true);
            }
            else if (!control.Crouch)
            {
                animator.SetBool(TransitionParameter.Dash.ToString(), false);
            }

            if (Constant)
            {
                ConstantMove(control, animator, stateInfo);
            }
            else {
                ControlledMove(control, animator, stateInfo);
            }

            if (control.Attack_Normal)
            {
                animator.SetBool(TransitionParameter.Attack.ToString(), true);
            }
        }


        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

            animator.SetBool(TransitionParameter.Move.ToString(), false);
        }

        private void ControlledMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo) {

            if (control.MoveRight && control.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }

            if (!control.MoveRight && !control.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }
            if (control.Spell)
            {
                animator.SetBool(SpellTypeParameters.Hadoken.ToString(), true);
            }

            if (control.Attack_Normal)
            {
                animator.SetBool(TransitionParameter.Attack.ToString(), true);
            }

            if (control.MoveRight)
            {
                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                if (!CheckFront(control))
                {
                    control.MoveForward(speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }

            if (control.MoveLeft)
            {
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                if (!CheckFront(control))
                {
                    control.MoveForward(speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }
        }

        private void ConstantMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo) {
            if (!CheckFront(control))
            {
                control.MoveForward(speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
            else {
                control.RIGID_BODY.velocity = new Vector3(0f, control.RIGID_BODY.velocity.y, 0f);
            }
        }


        bool CheckFront(CharacterControl control)
        {
            foreach (GameObject o in control.FrontSpheres)
            {
                Debug.DrawRay(o.transform.position, control.transform.forward * BlockDistance, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, control.transform.forward, out hit, BlockDistance))
                {
                    if (!control.RagdollParts.Contains(hit.collider)) {
                        if (!IsBodyPart(hit.collider)&& !Edge.IsEdge(hit.collider.gameObject)&&!Edge.IsEdgeChecker(hit.collider.gameObject)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        bool IsBodyPart(Collider col) {
            CharacterControl control = col.transform.root.GetComponent<CharacterControl>();

            if (control == null) {
                return false;
            }

            if (control.gameObject == col.gameObject) {
                return false;
            }

            if (control.RagdollParts.Contains(col)) {
                return true;
            }
            return false;
        }
    }
}

