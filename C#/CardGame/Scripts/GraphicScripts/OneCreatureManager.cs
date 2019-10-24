using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OneCreatureManager : MonoBehaviour 
{
    public Cards cardAsset;
    public OneCardManager PreviewManager;
    [Header("Text Component References")]
    public Text HealthText;
    public Text AttackText;
    [Header("Image References")]
    public Image CreatureGraphicImage;
    public Image CreatureGlowImage;

    void Awake()
    {
        if (cardAsset != null)
            ReadCreatureFromAsset();
    }

    private bool canAttackNow = false;
    public bool CanAttackNow
    {
        get
        {
            return canAttackNow;
        }

        set
        {
            canAttackNow = value;

            CreatureGlowImage.enabled = value;
        }
    }

    public void ReadCreatureFromAsset()
    {
        // Change the card graphic sprite
        CreatureGraphicImage.sprite = cardAsset.cardImage;

        /*AttackText.text = cardAsset.Attack.ToString();
        HealthText.text = cardAsset.MaxHealth.ToString();*/  //we don't have creature card for now

        if (PreviewManager != null)
        {
            PreviewManager.cardAsset = cardAsset;
            PreviewManager.ReadCardFromAsset();
        }
    }	

    public void TakeDamage(int amount, int healthAfter)
    {
        if (amount > 0)
        {
            UIDamageEffect.CreateDamageEffect(transform.position, amount);
            HealthText.text = healthAfter.ToString();
        }
    }
}
