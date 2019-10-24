using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

namespace HorrorGame
{
    public class CreateImage : MonoBehaviour
    {
        public Sprite[] sprites;
        public GameObject imgPrefab;
        public Jigsaw currentJigsaw;
        GameObject[] cells;
        

        public static CreateImage instance;

        private FirstPersonController firstPersonController;
        private AudioSource audioSource;
        public AudioClip pickupSound;

        [HideInInspector]
        public KeyItem rewardItem;
        [HideInInspector]
        public Door myDoor;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            sprites = null;
            firstPersonController = FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>();
            audioSource = firstPersonController.gameObject.GetComponent<AudioSource>();
        }

        public void InitImg() {

            for (int i = 0; i < sprites.Length; i++)
            {
                int index = Random.Range(i, sprites.Length);
                Sprite temp = sprites[i];
                sprites[i] = sprites[index];
                sprites[index] = temp;
            }

            cells = new GameObject[sprites.Length];

            for (int i = 0; i < sprites.Length; i++)
            {
                cells[i] = Instantiate(imgPrefab) as GameObject;
                cells[i].name = "tempimg_" + i.ToString();
                cells[i].transform.GetChild(0).GetComponent<Image>().sprite = sprites[i];
                cells[i].transform.SetParent(transform);
                cells[i].GetComponentInChildren<CreateDrag>().jigsawRF = GetComponent<RectTransform>();
                cells[i].transform.localScale = Vector3.one;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                currentJigsaw.audioSource.clip = currentJigsaw.openSound;
                currentJigsaw.audioSource.Play();
                currentJigsaw = null;
                sprites = null;
                for (int i = 0; i < transform.childCount; i++)
                {
                  Destroy(transform.GetChild(i).gameObject);
                }
                Time.timeScale = 1;
                firstPersonController.enabled = true;
                firstPersonController.transform.GetChild(0).GetComponentInChildren<LightSwitch>().enabled = true;
                firstPersonController.m_MouseLook.SetCursorLock(true);
                transform.parent.gameObject.SetActive(false);

            }
        }

        public bool IsFinished() {
            Sprite _sprite;
            foreach (GameObject cell in cells) {
                _sprite = cell.transform.GetChild(0).GetComponent<Image>().sprite;
                if (cell.name != _sprite.name) {
                    return false;
                }
            }
            return true;
        }


        public void PuzzleSolved() {
            currentJigsaw.solved = true;
            currentJigsaw.interactive = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
            currentJigsaw.GetComponent<MeshRenderer>().material.SetTexture("Texture2D_1066FD47", currentJigsaw.CompleteCoverSprite.texture);
            currentJigsaw.audioSource.clip = currentJigsaw.successSound;
            currentJigsaw.audioSource.Play();
            currentJigsaw = null;
            sprites = null;
            Time.timeScale = 1;
            StartCoroutine("CloseJigsaw");
            
        }

        IEnumerator CloseJigsaw() {
            yield return new WaitForSeconds(2f);

            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            firstPersonController.enabled = true;
            firstPersonController.transform.GetChild(0).GetComponentInChildren<LightSwitch>().enabled = true;
            firstPersonController.m_MouseLook.SetCursorLock(true);
            rewardItem.myDoor = myDoor;
            rewardItem.used = false;
            rewardItem.canBeRemoved = false;
            Inventory.Instance.Add(rewardItem);
            MessageManager.Instance.ShowMessage("It looks like a painting of Satan, a Key dropped out from it");
            audioSource.clip = pickupSound;
            audioSource.Play();
            transform.parent.gameObject.SetActive(false);
        }
    }
}
