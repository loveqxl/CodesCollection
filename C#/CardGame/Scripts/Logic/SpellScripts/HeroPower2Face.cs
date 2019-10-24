using UnityEngine;
using System.Collections;

public class HeroPower2Face : SpellEffect 
{

    public override void ActivateEffect(int specialAmount = 0, ICharacter target = null, Player player = null, CardLogic card = null)
    {
        int healthBefore = TurnManager.Instance.whoseTurn.otherPlayer.Health;
        new DealDamageCommand(TurnManager.Instance.whoseTurn.otherPlayer.PlayerID, 2, TurnManager.Instance.whoseTurn.otherPlayer.Health - 2,player,card,healthBefore).AddToQueue();
        TurnManager.Instance.whoseTurn.otherPlayer.Health -= 2;
    }
}
