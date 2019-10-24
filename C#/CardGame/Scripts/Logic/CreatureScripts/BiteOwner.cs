using UnityEngine;
using System.Collections;

public class BiteOwner : CreatureEffect
{  
    public BiteOwner(Player owner, CreatureLogic creature, int specialAmount): base(owner, creature, specialAmount)
    {}

    public override void RegisterEffect()
    {
        owner.EndTurnEvent += CauseEffect;
        Debug.Log("Registered bite effect!!!!");
    }

    public override void CauseEffect()
    {
        int healthBefore = owner.Health;
        Debug.Log("InCauseEffect: owner: "+ owner + " specialAmount: "+ specialAmount);
        new DealDamageCommand(owner.PlayerID, specialAmount, owner.Health - specialAmount,owner, null,healthBefore).AddToQueue();
        owner.Health -= specialAmount;
    }


}
