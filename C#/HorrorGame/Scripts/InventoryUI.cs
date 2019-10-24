using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

namespace HorrorGame
{
    public class InventoryUI : MonoBehaviour
    {
        public Transform itemsParent;
        public Transform useItemsParent;
        public GameObject inventoryUI;
        public Text descritionText;
        public string inventoryButton;
        public FirstPersonController firstPersonController;
        public GameObject safeUI;
        public GameObject jigsawUI;
        public GameObject useItemUI;
        public GameObject paperUI;
        public GameObject clockUI;
        public GameObject pauseMenu;
        public GameObject winView;
        public GameObject loseView;

        Inventory inventory;
        MessageManager messageManager;
        // Start is called before the first frame update

        InventorySlot[] slots;
        UseItemSlot[] useItemSlots;

        void Awake()
        {
            inventory = Inventory.Instance;
            messageManager = MessageManager.Instance;
            inventory.onItemChangedCallBack += UpdateUI;
            slots = itemsParent.GetComponentsInChildren<InventorySlot>();
            useItemSlots = useItemsParent.GetComponentsInChildren<UseItemSlot>();
            firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
            UpdateUI();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown(inventoryButton) && !safeUI.activeSelf && !jigsawUI.activeSelf && !useItemUI.activeSelf && !paperUI.activeSelf && !clockUI.activeSelf && !pauseMenu.activeSelf && !winView.activeSelf && !loseView.activeSelf)
            {
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                if (inventoryUI.activeSelf)
                {
                    descritionText.text = "";
                    Time.timeScale = 0;
                    firstPersonController.enabled = false;
                    firstPersonController.transform.GetChild(0).GetComponentInChildren<LightSwitch>().enabled = false;
                    firstPersonController.m_MouseLook.SetCursorLock(false);
                }
                else
                {
                    Time.timeScale = 1;
                    firstPersonController.enabled = true;
                    firstPersonController.transform.GetChild(0).GetComponentInChildren<LightSwitch>().enabled = true;
                    firstPersonController.m_MouseLook.SetCursorLock(true);
                }
            }
            if (Input.GetButtonDown("Cancel") && inventoryUI.activeSelf)
            {
                inventoryUI.SetActive(false);
                Time.timeScale = 1;
                firstPersonController.enabled = true;
                firstPersonController.transform.GetChild(0).GetComponentInChildren<LightSwitch>().enabled = true;
                firstPersonController.m_MouseLook.SetCursorLock(true);
            }
            else if(Input.GetButtonDown("Cancel") && !inventoryUI.activeSelf && !winView.activeSelf && !loseView.activeSelf && !safeUI.activeSelf && !jigsawUI.activeSelf && !useItemUI.activeSelf)
            { 
     
                    if (!pauseMenu.activeSelf)
                    {
                        Time.timeScale = 0;
                        pauseMenu.SetActive(true);
                        firstPersonController.enabled = false;
                        firstPersonController.transform.GetChild(0).GetComponentInChildren<LightSwitch>().enabled = false;
                        firstPersonController.m_MouseLook.SetCursorLock(false);
                    }
                    else
                    {
                        ClosePauseMenu();
                    } 
            }
        }


        public void ClosePauseMenu()
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            firstPersonController.enabled = true;
            firstPersonController.transform.GetChild(0).GetComponentInChildren<LightSwitch>().enabled = true;
            firstPersonController.m_MouseLook.SetCursorLock(true);
        }

        public void QuitGame() {
            Time.timeScale = 1;
            Application.Quit();
        }

        public void GoToMainMenu() {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }

        void UpdateUI() {
            List<Item> putItems = new List<Item>();
            for (int h = 0; h < inventory.items.Count; h++)
            {
                if (inventory.items[h].canBePut) {
                    putItems.Add(inventory.items[h]);                 
                }
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if (i < inventory.items.Count)
                {
                    slots[i].AddItem(inventory.items[i]);
                }
                else {
                    slots[i].ClearSlot();
                }
            }

            for (int j = 0; j < useItemSlots.Length; j++) {
                if (j < putItems.Count)
                {
                    useItemSlots[j].AddItem(putItems[j]);
                }
                else {
                    useItemSlots[j].ClearSlot();
                }
            }
        }
    }
}
