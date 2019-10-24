using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6 {
    public class CharacterSelectLight : MonoBehaviour
    {

        public Light light;
        // Start is called before the first frame update
        void Start()
        {
            light = GetComponent<Light>();
        }

    }
}

