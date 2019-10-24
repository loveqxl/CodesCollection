using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class CardLogic: IIdentifiable
{
    public Player owner;
    public int UniqueCardID; 

    public Cards ca;
    //public GameObject VisualRepresentation;

    private int baseManaCost;
    public SpellEffect effect;

    public int actAmount=0;

    public int ID
    {
        get{ return UniqueCardID; }
    }

    public int CurrentManaCost{ get; set; }

    public bool CanBePlayed
    {
        get
        {
            bool ownersTurn = (TurnManager.Instance.whoseTurn == owner);
            // for spells the amount of characters on the field does not matter
            bool fieldNotFull = true;
           /* we don't have creature card now// but if this is a creature, we have to check if there is room on board (table)
            if (ca.MaxHealth > 0)
                fieldNotFull = (owner.table.CreaturesOnTable.Count < 7); */
            //Debug.Log("Card: " + ca.name + " has params: ownersTurn=" + ownersTurn + "fieldNotFull=" + fieldNotFull + " hasMana=" + (CurrentManaCost <= owner.ManaLeft));
            return ownersTurn && fieldNotFull && (CurrentManaCost <= owner.ManaLeft);
        }
    }

    public CardLogic(Cards ca)
    {
        this.ca = ca;
        UniqueCardID = IDFactory.GetUniqueID();
        //UniqueCardID = IDFactory.GetUniqueID();
        //baseManaCost = ca.ManaCost;
        ResetManaCost();
        if (ca.SpellScriptName!= null && ca.SpellScriptName!= "")
        {
            effect = System.Activator.CreateInstance(System.Type.GetType(ca.SpellScriptName)) as SpellEffect;
        }
        CardsCreatedThisGame.Add(UniqueCardID, this);
    }

    public void ResetManaCost()
    {
        CurrentManaCost = baseManaCost;
    }

    public void UpdateNumber(int bonusNumber) {
        //TODO
        new CardUpdateNumberCommand(this, bonusNumber).AddToQueue();
    }

    // STATIC (for managing IDs)
    public static Dictionary<int, CardLogic> CardsCreatedThisGame = new Dictionary<int, CardLogic>();

}
