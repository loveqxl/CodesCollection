using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6 {
    public class EdgeChecker : MonoBehaviour
    {
        public bool IsGrabbingEdge;
        public Edge GrabbedEdge;
        Edge CheckEdge = null;
        private void OnTriggerEnter(Collider other)
        {
            CheckEdge = other.gameObject.GetComponent<Edge>();
            if (CheckEdge != null) {
                IsGrabbingEdge = true;
                GrabbedEdge = CheckEdge;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            CheckEdge = other.gameObject.GetComponent<Edge>();
            if (CheckEdge != null)
            {
                IsGrabbingEdge = false;
            }
        }
    }
}

