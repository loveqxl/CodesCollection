using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Week6
{
    public class KeyboardInput : MonoBehaviour
    {

      
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.W))
            {
                VirtualInputManager.Instance.MoveUp = true;
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                VirtualInputManager.Instance.MoveUp = false;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                VirtualInputManager.Instance.MoveDown = true;
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                VirtualInputManager.Instance.MoveDown = false;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                VirtualInputManager.Instance.MoveRight = true;
            }
            else if(Input.GetKeyUp(KeyCode.D)){
                VirtualInputManager.Instance.MoveRight = false;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                VirtualInputManager.Instance.MoveLeft = true;
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                VirtualInputManager.Instance.MoveLeft = false;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                VirtualInputManager.Instance.Jump = true;
  
            }
            else {
                VirtualInputManager.Instance.Jump = false;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                VirtualInputManager.Instance.Crouch = true;

            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                VirtualInputManager.Instance.Crouch = false;
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                VirtualInputManager.Instance.Attack = true;

            }
            else {
                VirtualInputManager.Instance.Attack = false;
            }

        }
    }
}