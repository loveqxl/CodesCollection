using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

namespace HorrorGame
{
    public class PaperUI : MonoBehaviour
    {
        public Image paperBG;

        public Text descriptionText;

        public PaperItem currentPaper;

        public FirstPersonController firstPersonController;
        // Start is called before the first frame update
        void Awake()
        {
            paperBG.sprite = null;
            descriptionText.text = "";
            firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        }

        // Update is called once per frame
        void Update()
        {
            if ((Input.GetButtonDown("Cancel")|| Input.GetButtonDown("Fire1")) && gameObject.activeSelf)
            {
                ClosePaperUI();
            }
        }

        public void ClosePaperUI()
        {
            currentPaper.TriggerEvent();
            currentPaper.canBeRemoved = true;
            gameObject.SetActive(false);
            paperBG.sprite = null;
            descriptionText.text = "";
        }
    }
}
