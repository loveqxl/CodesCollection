using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace HorrorGame
{
    [CreateAssetMenu(fileName = "New PaperItem", menuName = "HorrorGame/ClockItem")]
    public class ClockItem : Item
    {
        public Sprite BGsprite;

        public GameObject clockUI;

        public FirstPersonController firstPersonController;


        public override void Use()
        {

            clockUI.SetActive(true);
            clockUI.GetComponent<ClockUI>().currentClock = this;
            clockUI.GetComponent<ClockUI>().clockBG.sprite = BGsprite;

        }

    }
}
