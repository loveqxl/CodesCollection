using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorrorGame
{
    public class Inventory : Singleton<Inventory>
    {

        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallBack;

        public int space = 20;

        public List<Item> items = new List<Item>();

        public void Start()
        {
           
        }

        public bool Add(Item item)
        {
            if (!item.isDefaultItem)
            {
                if (items.Count >= space) {
                    Debug.Log("No enough space");
                    return false;
                }
                items.Add(item);

                if (onItemChangedCallBack != null)
                {
                    onItemChangedCallBack.Invoke();
                }
            }
            return true;
        }

        public void Remove(Item item)
        {
            items.Remove(item);
            if (onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }
        }
    }
}
