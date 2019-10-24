using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6 {
    public class BlackScreen : MonoBehaviour
    {
        public PlayGame playGameManager;

        private void GoToNextScene() {
            playGameManager.GoToNextScene();
        }
    }
}
