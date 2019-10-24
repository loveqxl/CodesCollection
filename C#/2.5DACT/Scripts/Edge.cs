using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Week6 {
    public class Edge : MonoBehaviour
    {
        public Vector3 Offset = new Vector3(0f, -12f, -1.023509f);
        public Vector3 EndPosition = new Vector3 (0f,0.05f,0.3f);
        public static bool IsEdge(GameObject obj) {
            if (obj.GetComponent<Edge>() == null) {
                return false;
            }
            return true;
        }

        public static bool IsEdgeChecker(GameObject obj) {
            if (obj.GetComponent<EdgeChecker>() == null) {
                return false;
            }
            return true;
        }
    }


}

