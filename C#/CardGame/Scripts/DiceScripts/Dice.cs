using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public Camera diceCamera;
    public Rigidbody rb;
    public static Vector3 diceVelocity;
    public int DiceNumber = 0;
    public TurnManager turnManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        diceVelocity = rb.velocity;

    }

    public void RollDice() {
        
        StartCoroutine(StartRoll());
        //return dicenumber
       
        
    }

    private IEnumerator StartRoll() {
        if (turnManager.whoseTurn.table.CardsOnTable.Count != 0)
        {
            diceCamera.enabled = true;
            if (DiceNumber != 0)
            {
                DiceNumber = 0;
                float dirX = Random.Range(0, 500);
                float dirY = Random.Range(0, 500);
                float dirZ = Random.Range(0, 500);
                transform.position = new Vector3(0, 3, -20);
                transform.rotation = Quaternion.identity;
                rb.AddForce(transform.up * 500);
                rb.AddTorque(dirX, dirY, dirZ);
            }
            yield return new WaitForSeconds(6);
            diceCamera.enabled = false;
        }
        turnManager.whoseTurn.bonusNumber = DiceNumber;
        Command.CommandExecutionComplete();

    }
}
