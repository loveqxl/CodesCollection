using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Week6 {
    [CustomEditor(typeof(MaterialChanger))]
    public class MaterialChangerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            DrawDefaultInspector();
            MaterialChanger materialChanger = (MaterialChanger)target;
            if (GUILayout.Button("Change Material")) {
                materialChanger.ChangeMaterial();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
