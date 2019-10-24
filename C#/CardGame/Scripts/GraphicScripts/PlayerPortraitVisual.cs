using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerPortraitVisual : MonoBehaviour {

    // TODO : get ID from players when game starts
    public Player player;
    private int playerID;
    public Character charAsset;
    [Header("Text Component References")]
    //public Text NameText;
    public Text CharatcterName;
    public Text HealthText;
    [Header("Image References")]
    public Image PortraitImage;
    public Image PortraitBackgroundImage;
    public Image PortraitGlowImage;

    private void Awake()
    {
        if (charAsset != null) {
            ApplyLookFromAsset();
        }
        playerID = player.ID;
    }

    public void ApplyLookFromAsset()
    {
        CharatcterName.text = charAsset.ClassName.ToString();
        HealthText.text = charAsset.MaxHealth.ToString();
        PortraitImage.sprite = charAsset.AvatarImage;
        PortraitBackgroundImage.sprite = charAsset.AvatarBGImage;
        PortraitGlowImage.sprite = charAsset.PortraitGlowImage;
        PortraitBackgroundImage.color = charAsset.AvatarBGTint;

    }

    public void TakeDamage(int amount, int healthAfter,int healthBefore)
    {
        if (amount > 0)
        {
            float dirX = Random.Range(-0.5f, 0.5f);
            float dirY = Random.Range(-0.5f, 0.5f);
            Vector3 offset = new Vector3(dirX,dirY,0);
            UIDamageEffect.CreateDamageEffect(transform.position+offset, amount);
            StartCoroutine(HealthNmberChange(healthAfter,healthBefore));

        }
    }


        
 
    IEnumerator HealthNmberChange(int changedNumber,int beforeNumber)
    {
        
        int healthnum = beforeNumber;
        while (healthnum > changedNumber)
        {
            healthnum--;
            HealthText.text = healthnum.ToString();
            yield return new WaitForSeconds(0.05f);
        }
        HealthText.text = player.Health.ToString();
    }

    public void Explode()
    {

        Instantiate(GlobalSettings.Instance.ExplosionPrefab, transform.position, Quaternion.identity);
        Sequence s = DOTween.Sequence();
        s.PrependInterval(2f);
        s.OnComplete(() => GlobalSettings.Instance.GameOverCanvas.SetActive(true));
      
    }



}
