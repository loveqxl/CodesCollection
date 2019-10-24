using UnityEngine;
using System.Collections;

public class PlayASpellCardCommand : Command
{
    private CardLogic card;
    private Player p;

    public PlayASpellCardCommand(Player p, CardLogic card)
    {
        this.p = p;
        this.card = card;

    }

    public override void StartCommandExecution()
    {


  
        // remove and destroy the card in hand 
        p.PArea.handVisual.PlayASpellFromHand(card.UniqueCardID);

    }
}
