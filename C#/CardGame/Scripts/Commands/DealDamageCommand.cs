using UnityEngine;
using System.Collections;

public class DealDamageCommand : Command {

    private int targetID;
    private int amount;
    private int healthAfter;
    private Player player;
    private CardLogic card;
    private int healthBefore;
    public DealDamageCommand( int targetID, int amount, int healthAfter, Player player, CardLogic card,int healthBefore)
    {
        this.targetID = targetID;
        this.amount = amount;
        this.healthAfter = healthAfter;
        this.player = player;
        this.card = card;
        this.healthBefore = healthBefore;
    }

    public override void StartCommandExecution()
    {

        GameObject target = IDHolder.GetGameObjectWithID(targetID);
        if (targetID == GlobalSettings.Instance.LowPlayer.PlayerID || targetID == GlobalSettings.Instance.TopPlayer.PlayerID)
        {
            // target is a hero
            target.GetComponent<PlayerPortraitVisual>().TakeDamage(amount,healthAfter,healthBefore);
        }
        else
        {
            // target is a creature
            target.GetComponent<OneCreatureManager>().TakeDamage(amount, healthAfter);
        }

        CommandExecutionComplete();
    }
}
