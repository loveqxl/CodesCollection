using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorrorGame {
    public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        private static T _Instance;

        public static T Instance {
            get {
                //_Instance = (T)FindObjectOfType(typeof(T));
                if (_Instance == null) {
                    GameObject obj = new GameObject();
                    _Instance = obj.AddComponent<T>();
                    obj.name = typeof(T).ToString();
                }
                return _Instance;
            }
        }
    }
}

