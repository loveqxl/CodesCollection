using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{
    public class DeathAnimationLoader : MonoBehaviour
    {
        public List<DeathAnimationData> DeathAnimationDataList = new List<DeathAnimationData>();
        public RuntimeAnimatorController NormalAnimator;
        public RuntimeAnimatorController HittedAnimator;
    }
}

