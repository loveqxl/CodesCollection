using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnCommand : Command
{
    private Player whoseTurn;
    private Player whoGoesFirst;
    private Player whoGoesSecond;
    private Dice dice;
    public EndTurnCommand(Player whoseTurn, Player whoGoesFirst, Player whoGoesSecond,Dice dice)
    {
        this.whoseTurn = whoseTurn;
        this.whoGoesFirst = whoGoesFirst;
        this.whoGoesSecond = whoGoesSecond;
        this.dice = dice;
    }

    public override void StartCommandExecution()
    {
        //update cards according to whoseTurn.bonusNumber;
        foreach (CardLogic card in whoseTurn.table.CardsOnTable) {
            card.UpdateNumber(whoseTurn.bonusNumber);
        }
        if (whoseTurn == whoGoesSecond)
        {
            foreach (Command com in whoGoesFirst.commandsWaitinglist)
            {
                com.AddToQueue();
                
            }
            whoGoesFirst.commandsWaitinglist.Clear();
            foreach (Command com in whoGoesSecond.commandsWaitinglist)
            {
                com.AddToQueue();
            }
            whoGoesSecond.commandsWaitinglist.Clear();
            
        }
        new StartATurnCommand(whoseTurn.otherPlayer).AddToQueue();
        Command.CommandExecutionComplete();
    }
}
