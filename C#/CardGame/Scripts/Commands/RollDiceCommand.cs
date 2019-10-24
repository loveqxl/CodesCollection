using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDiceCommand : Command
{
    private Dice dice;

    public RollDiceCommand(Dice _dice) {
        this.dice = _dice;
    }

    public override void StartCommandExecution()
    {
        dice.RollDice();
    }

}
