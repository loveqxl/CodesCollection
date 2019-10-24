using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace HorrorGame
{
    [CreateAssetMenu(fileName = "New PaperItem", menuName = "HorrorGame/PaperItem")]
    public class PaperItem : Item
    {
        public Sprite BGsprite;

        public GameObject paperUI;

        public FirstPersonController firstPersonController;

        public JumpScareObject[] jumpScareObjs;

        [TextArea(5,10)]
        public string content;

        public override void Use()
        {

                paperUI.SetActive(true);
                paperUI.GetComponent<PaperUI>().currentPaper = this;
                paperUI.GetComponent<PaperUI>().paperBG.sprite = BGsprite;
                paperUI.GetComponent<PaperUI>().descriptionText.text = content;

        }

        public void TriggerEvent()
        {
            if (jumpScareObjs != null)
            {
                foreach (JumpScareObject obj in jumpScareObjs)
                {
                    obj.TriggerJumpScares();
                }
            }
        }
    }
}
