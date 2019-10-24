using UnityEngine;
using System.Collections;

public class GiveManaBonus: SpellEffect 
{
    public override void ActivateEffect(int specialAmount = 0, ICharacter target = null,Player player = null, CardLogic card = null)
    {
        TurnManager.Instance.whoseTurn.GetBonusMana(specialAmount);
    }
}
