using UnityEngine;
using System.Collections;

public class SpellEffect
{
    public virtual void ActivateEffect(int specialAmount = 0, ICharacter target = null, Player player= null,CardLogic card=null)
    {
        Debug.Log("No Spell effect with this name found! Check for typos in CardAssets");
    }
        
}
