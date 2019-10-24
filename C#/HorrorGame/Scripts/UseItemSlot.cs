using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HorrorGame
{
    public class UseItemSlot : MonoBehaviour, IPointerEnterHandler
    {
        public Image icon;
        public Text descriptionText;
        Item item;

        public void AddItem(Item newItem)
        {
            item = newItem;
            icon.sprite = item.icon;
            icon.enabled = true;
        }

        public void ClearSlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
        }

        public void PutItem()
        {
            if (item != null)
            {
                PuttingPlace puttingPlace = transform.parent.parent.GetComponentInParent<UseItemUI>().currentPuttingPlace;
                MessageManager.Instance.ShowMessage("You Put " + item.name + " on the " + puttingPlace.name);
                item.Put(puttingPlace);
                transform.parent.parent.GetComponentInParent<UseItemUI>().CloseUseItemUI();
            }

        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            if (item != null)
            {
                descriptionText.text = item.description;
            }
            else
            {
                descriptionText.text = "";
            }
        }
    }
}
