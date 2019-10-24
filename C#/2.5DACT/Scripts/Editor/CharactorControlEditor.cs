using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Week6 {
    [CustomEditor(typeof(CharacterControl))]
    public class CharactorControlEditor : Editor {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            CharacterControl control = (CharacterControl)target;

            if (GUILayout.Button("Setup RagdollParts (BodyParts)")) {
                control.SetRagdollParts();
            }
        }



    }
}
