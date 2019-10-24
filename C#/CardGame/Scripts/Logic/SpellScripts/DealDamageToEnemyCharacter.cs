using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageToEnemyCharacter : SpellEffect
{


    public override void ActivateEffect(int specialAmount = 0, ICharacter target = null, Player player = null, CardLogic card = null)
    {
        int healthBefore = target.Health;
        new DealDamageCommand(target.ID, specialAmount, healthAfter: target.Health - specialAmount,player,card,healthBefore).AddToQueue();
        target.Health -= specialAmount;

    }
}