using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6 {
    public class ManualInput : MonoBehaviour
    {
        private CharacterControl characterControl;

        private void Awake()
        {
            characterControl = this.gameObject.GetComponent<CharacterControl>();
        }

        private void Update()
        {
            if (VirtualInputManager.Instance.Jump)
            {
                characterControl.Jump = true;
                if (VirtualInputManager.Instance.MoveRight||VirtualInputManager.Instance.MoveLeft)
                {
                    characterControl.JumpForward = true;
                }
            }
            else
            {
                characterControl.Jump = false;
                characterControl.JumpForward = false;
            }

            if (VirtualInputManager.Instance.MoveUp)
            {
                characterControl.MoveUp = true;
            }
            else
            {
                characterControl.MoveUp = false;
            }

            if (VirtualInputManager.Instance.MoveDown)
            {
                characterControl.MoveDown = true;
            }
            else
            {
                characterControl.MoveDown = false;
            }

            if (VirtualInputManager.Instance.MoveRight)
            {
                characterControl.MoveRight = true;
            }
            else {
                characterControl.MoveRight = false;
            }

            if (VirtualInputManager.Instance.MoveLeft)
            {
                characterControl.MoveLeft = true;
            }
            else
            {
                characterControl.MoveLeft = false;
            }

            if (VirtualInputManager.Instance.Crouch)
            {
                characterControl.Crouch = true;
            }
            else
            {
                characterControl.Crouch = false;
            }
           

            if (VirtualInputManager.Instance.Attack)
            {
                characterControl.Attack = true;
            }
            else if(!VirtualInputManager.Instance.Attack) {
                characterControl.Attack = false;
                //characterControl.Attack_Normal = false;
            }

            if (!VirtualInputManager.Instance.MoveLeft && !VirtualInputManager.Instance.MoveRight && !VirtualInputManager.Instance.Attack && !VirtualInputManager.Instance.Jump && !VirtualInputManager.Instance.Crouch)
            {
                characterControl.Idle = true;
            }
            else {
                characterControl.Idle = false;
            }
        }

    }

}
