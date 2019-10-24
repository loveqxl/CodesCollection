using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6 {
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public bool MoveUp;
        public bool MoveDown;

        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
        public bool Attack;
        public bool Crouch;
    }
}

