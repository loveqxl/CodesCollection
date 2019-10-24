using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadCheckCommand : Command
{
    private int health1;
    private int health2;
    private Text gameOverText;
    private Player player1;
    private Player player2;
    private Player whoseTurnNext;

    public DeadCheckCommand(Player whoseTurnNext) {
        player1 =GlobalSettings.Instance.LowPlayer.GetComponent<Player>();
        player2 =GlobalSettings.Instance.TopPlayer.GetComponent<Player>();
        health1 = player1.Health;
        health2 = player2.Health;
        gameOverText = GlobalSettings.Instance.GameOverCanvas.GetComponentInChildren<Text>();
        this.whoseTurnNext = whoseTurnNext;
    }

    public override void StartCommandExecution()
    {
        
        if (health1 <= 0 && health2 > 0)
        {

            gameOverText.text = player2.name + " Win!";
            player1.Die();
        }
        else if (health1 > 0 && health2 <= 0)
        {
            gameOverText.text = player1.name + " Win!";
            player2.Die();
        }
        else if (health1 <= 0 && health2 <= 0)
        {
            gameOverText.text = "Double K.O.!";
            player1.Die();
            player2.Die();
        }
        else {
            TurnManager.Instance.whoseTurn = whoseTurnNext;
            Command.CommandExecutionComplete(); } 
    }

}
