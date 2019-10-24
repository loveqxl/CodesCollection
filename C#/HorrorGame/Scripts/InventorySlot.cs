using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HorrorGame
{
    public class InventorySlot : MonoBehaviour, IPointerEnterHandler
    {
        public Image icon;
        public Button removeButton;
        public Text descriptionText;
        public Text ItemCountText;

        Item item;

        private void Awake()
        {
            removeButton.interactable = false;
        }

        public void AddItem(Item newItem) {
            item = newItem;
            icon.sprite = item.icon;
            icon.enabled = true;
            ItemCountText.enabled = true;
            ItemCountText.text = item.Count.ToString();
        removeButton.gameObject.SetActive(true);
        }

        public void ClearSlot() {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            ItemCountText.enabled = false;
            removeButton.interactable = false;
            removeButton.gameObject.SetActive(false);
        }

        public void OnRemoveButton() {
            Inventory.Instance.Remove(item);
        }

        public void UseItem() {
            if (item != null) {
                item.Use();
            }
        }

        private void Update()
        {
            if (item != null)
            {
                if (item.canBeRemoved && !removeButton.interactable)
                {
                    removeButton.interactable = true;
                }
            }
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            if (item != null)
            {
                descriptionText.text = item.description;
            }
            else {
                descriptionText.text = "";
            }
        }
    }
}
