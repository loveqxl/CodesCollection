using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorrorGame
{
    [CreateAssetMenu(fileName ="New Item",menuName ="HorrorGame/Item")]
    public class Item : ScriptableObject
    {
        new public string name = "New Item";
        public Sprite icon = null;
        [TextArea(3,5)]
        public string description = "";
        public GameObject prefab;
        public bool isDefaultItem = false;
        public bool canBeRemoved = false;
        public bool canBePut=false;
        public int Count=1;

        public virtual void Use() {

        }

        public void Put(PuttingPlace puttingPlace)
        {
            if (puttingPlace.puttingSlot.childCount == 0)
            {
                
                GameObject obj = (GameObject)Instantiate<GameObject>(prefab);
                obj.transform.parent = puttingPlace.puttingSlot;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.name = name;
                Inventory.Instance.Remove(this);
            }
            else {
                puttingPlace.puttingSlot.GetChild(0).GetComponent<InteractiveObject>().PickUp();
                GameObject obj = (GameObject)Instantiate<GameObject>(prefab);
                obj.transform.parent = puttingPlace.puttingSlot;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.name = name;
                Inventory.Instance.Remove(this);
            }
            puttingPlace.audioSource.clip = puttingPlace.puttingSound;
            puttingPlace.audioSource.Play();

        }
    }
}
