using UnityEngine;
using System.Collections;

public class DealDamageToTarget : SpellEffect 
{
    

    public override void ActivateEffect(int specialAmount = 0, ICharacter target = null, Player player=null,CardLogic card=null)
    {
        int healthBefore = target.Health;
        new DealDamageCommand(target.ID, specialAmount, healthAfter: target.Health - specialAmount, player , card, healthBefore ).AddToQueue();
        target.Health -= specialAmount;

        player.PArea.tableVisual.PlayASpellFromTable(card.ID);
        player.table.CardsOnTable.Remove(card);
    }
}
