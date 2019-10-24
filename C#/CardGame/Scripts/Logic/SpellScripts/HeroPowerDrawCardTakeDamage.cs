using UnityEngine;
using System.Collections;

public class HeroPowerDrawCardTakeDamage : SpellEffect {

    public override void ActivateEffect(int specialAmount = 0, ICharacter target = null, Player player = null, CardLogic card = null)
    {
        int healthBefore = TurnManager.Instance.whoseTurn.Health;
        // Take 2 damage
        new DealDamageCommand(TurnManager.Instance.whoseTurn.PlayerID, 2, TurnManager.Instance.whoseTurn.Health - 2,player,card,healthBefore).AddToQueue();
        TurnManager.Instance.whoseTurn.Health -= 2;
        // Draw a card
        TurnManager.Instance.whoseTurn.DrawACard();

    }
}
