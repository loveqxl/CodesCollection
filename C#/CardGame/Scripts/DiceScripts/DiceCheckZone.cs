using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZone : MonoBehaviour
{
    Vector3 diceVelocity;
    public Dice dice;
    // Start is called before the first frame update

    private void FixedUpdate()
    {
        diceVelocity = Dice.diceVelocity;
    }

    private void OnTriggerStay(Collider col)
    {
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f && dice.DiceNumber==0) {
            switch (col.gameObject.name) {
                case "Side1":
                    dice.DiceNumber = 6;
                    break;
                case "Side2":
                    dice.DiceNumber = 5;
                    break;
                case "Side3":
                    dice.DiceNumber = 4;
                    break;
                case "Side4":
                    dice.DiceNumber = 3;
                    break;
                case "Side5":
                    dice.DiceNumber = 2;
                    break;
                case "Side6":
                    dice.DiceNumber = 1;
                    break;
            }
            StopAllCoroutines();
        }
    }
}
