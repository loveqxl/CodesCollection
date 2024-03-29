﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DragSpellNoTarget: DraggingActions{

    private int savedHandSlot;
    private WhereIsTheCardOrCreature whereIsCard;

    void Awake()
    {
        whereIsCard = GetComponent<WhereIsTheCardOrCreature>();
    }

    public override void OnStartDrag()
    {
        savedHandSlot = whereIsCard.Slot;

        whereIsCard.VisualState = VisualStates.Dragging;
        whereIsCard.BringToFront();

    }

    public override void OnDraggingInUpdate()
    {
        
    }

    public override void OnEndDrag()
    {
        // 1) Check if we are holding a card over the table
        if (DragSuccessful())
        {
            // play this card
            playerOwner.PlayASpellFromHand(GetComponent<IDHolder>().UniqueID, playerOwner.otherPlayer.ID);
        }
        else
        {
            // Set old sorting order 
            whereIsCard.Slot = savedHandSlot;
            if(tag.Contains("Low"))
            whereIsCard.VisualState = VisualStates.LowHand;
            else
                whereIsCard.VisualState = VisualStates.TopHand;
            // Move this card back to its slot position
            HandVisual PlayerHand = playerOwner.PArea.handVisual;
            Vector3 oldCardPos = PlayerHand.slots.Children[savedHandSlot].transform.localPosition;
            transform.DOLocalMove(oldCardPos, 1f);
        } 
    }

    protected override bool DragSuccessful()
    {
        bool TableNotFull = (TurnManager.Instance.whoseTurn.table.CardsOnTable.Count < 4);

        return TableVisual.CursorOverSomeTable; //&& TableNotFull;
    }


}
