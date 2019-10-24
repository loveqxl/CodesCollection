using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEffectCommand : Command
{
    private CardLogic playedCard;
    private ICharacter target;
    private Player owner;
    private Table table;

    public TriggerEffectCommand(CardLogic playedCard, ICharacter target, Player owner,Table table) {
        this.playedCard = playedCard;
        this.target = target;
        this.owner = owner;
        this.table = table;
    }
    public override void StartCommandExecution()
    {

        if (playedCard.effect != null)
        {
            if (playedCard.ca.cardType == CardType.Spell)
            {
                playedCard.effect.ActivateEffect(playedCard.actAmount, target, owner , playedCard);
            } else if (playedCard.ca.cardType == CardType.Weapon) {
                playedCard.effect.ActivateEffect(playedCard.actAmount, target, owner , playedCard);
            }
        }
        else
        {
            Debug.LogWarning("No effect found on card " + playedCard.ca.name);
        }



    }
}
