using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

namespace HorrorGame
{
    public class ClockUI : MonoBehaviour
    {
        public Image clockBG;

        public ClockItem currentClock;

        public FirstPersonController firstPersonController;
        // Start is called before the first frame update
        void Awake()
        {
            clockBG.sprite = null;
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
            currentClock.canBeRemoved = true;
            gameObject.SetActive(false);
            clockBG.sprite = null;
        }
    }
}
