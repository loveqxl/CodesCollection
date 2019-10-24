using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6 {
    public class PoolManager : Singleton<PoolManager>
    {
        public Dictionary<PoolObjectType, List<GameObject>> PoolDictionary = new Dictionary<PoolObjectType, List<GameObject>>();

        public void SetupDictionary() {
            PoolObjectType[] arr = System.Enum.GetValues(typeof(PoolObjectType))as PoolObjectType[];
            foreach (PoolObjectType p in arr) {
                if (!PoolDictionary.ContainsKey(p)) {
                    PoolDictionary.Add(p, new List<GameObject>());
                }
            }
        }

        public GameObject GetObject(PoolObjectType objType) {
            if (PoolDictionary.Count == 0) {
                SetupDictionary();
            }

            List<GameObject> list = PoolDictionary[objType];
            GameObject obj = null;
            if (list.Count > 0)
            {
                obj = list[0];
                list.RemoveAt(0);
            }
            else {
                obj = PooledObjectLoader.InstantiatePrefab(objType).gameObject;
            }

            return obj;
        }

        public void AddObject(PoolObject obj) {
            List<GameObject> list = PoolDictionary[obj.poolObjectType];
            list.Add(obj.gameObject);
            obj.gameObject.SetActive(false);
        }

    }
}

