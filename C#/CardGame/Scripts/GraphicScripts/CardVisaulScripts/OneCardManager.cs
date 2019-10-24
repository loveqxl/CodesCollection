using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// holds the refs to all the Text, Images on the card
public class OneCardManager : MonoBehaviour {

    public Cards cardAsset;
    public OneCardManager PreviewManager;
    [Header("Text Component References")]
    public Text NameText;
    public Text DescriptionText;
    public Text AmountText;
    public Text CardTypeText;   
    [Header ("GameObject References")]
    public GameObject CardTypeIcon;
    [Header("Image References")]
    public Image CardGraphicImage;
    public Image CardBodyImage;
    public Image CardFaceFrameImage;
    public Image CardTypeRibbonImage;
    public Image CardFaceGlowImage;
    public Image CardBackGlowImage;

    void Awake()
    {
        if (cardAsset != null)
            ReadCardFromAsset();
    }

    private bool canBePlayedNow = false;
    public bool CanBePlayedNow
    {
        get
        {
            return canBePlayedNow;
        }

        set
        {
            canBePlayedNow = value;

            CardFaceGlowImage.enabled = value;
        }
    }

    public void ReadCardFromAsset()
    {
        // universal actions for any Card
        // 1) apply tint
        if (cardAsset.character != null)
        {
            if (!cardAsset.character.ClassCardTint.Equals(new Color32(0, 0, 0, 0)))
            {
                CardBodyImage.color = cardAsset.character.ClassCardTint;
                CardFaceFrameImage.color = cardAsset.character.ClassCardTint;
            }

            if (!cardAsset.character.ClassRibbonsTint.Equals(new Color32(0, 0, 0, 0)))
            {
                CardTypeRibbonImage.color = cardAsset.character.ClassRibbonsTint;
            }
            else if (cardAsset.cardType == CardType.Spell)
            {
                if (cardAsset.spellType == SpellType.Damage)
                {
                    CardTypeRibbonImage.color = new Color32(210, 149, 231, 255);
                }
                else if (cardAsset.spellType == SpellType.Heal)
                {
                    CardTypeRibbonImage.color = new Color32(83, 226, 154, 255);
                }
                else
                {
                    CardTypeRibbonImage.color = new Color32(249, 255, 108, 255);
                }
                }
            else if (cardAsset.cardType == CardType.Weapon) {
                CardTypeRibbonImage.color = new Color32(226, 82, 111, 255);
            }
        }
        else if (cardAsset.cardType == CardType.Spell)
        {
            CardFaceFrameImage.color = Color.white;
            if (cardAsset.spellType == SpellType.Damage)
            {
                CardTypeIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Ravenmore/scroll");
                CardTypeRibbonImage.color = new Color32(210, 149, 231, 255);
            }
            else if (cardAsset.spellType == SpellType.Heal)
            {
                CardTypeIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Ravenmore/tools");
                CardTypeRibbonImage.color = new Color32(83, 226, 154, 255);
            }
            else {
                CardTypeIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Ravenmore/tome");
                CardTypeRibbonImage.color = new Color32(249, 255, 108, 255);
            }
            }
        else if(cardAsset.cardType == CardType.Weapon)
        {
            CardFaceFrameImage.color = Color.white;
            CardTypeRibbonImage.color = new Color32(226,82,111,255);
            CardTypeIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Ravenmore/upg_dagger");
        }

        if (cardAsset.cardType == CardType.Spell) {
            AmountText.text = cardAsset.basicAmount.ToString();
        } else if (cardAsset.cardType == CardType.Weapon) {
            AmountText.text = cardAsset.basicAttack.ToString();
        }
        // 2) add card name
        NameText.text = cardAsset.name;
        
        // 4) add description
        DescriptionText.text = cardAsset.description;
        // 5) Change the card graphic sprite
        CardGraphicImage.sprite = cardAsset.cardImage;
        CardTypeText.text = cardAsset.cardType.ToString();
        

        if (PreviewManager != null)
        {
            // this is a card and not a preview
            // Preview GameObject will have OneCardManager as well, but PreviewManager should be null there
            PreviewManager.cardAsset = cardAsset;
            PreviewManager.ReadCardFromAsset();
        }
    }

    public void ShowNumberChange(int changedNumber) {
        StartCoroutine(NmberChange(changedNumber));
    }

    IEnumerator NmberChange(int changedNumber) {
        AmountText.text = "0";
        int amount = 0;
        while (amount < changedNumber)
        {
            amount++;
            AmountText.text = amount.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        if (PreviewManager != null)
        {
            PreviewManager.AmountText.text = amount.ToString();
        }
        Command.CommandExecutionComplete();
    }
}
