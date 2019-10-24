using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// an enum to store the info about where this object is
public enum VisualStates
{
    Transition,
    LowHand, 
    TopHand,
    LowTable,
    TopTable,
    Dragging
}

public class WhereIsTheCardOrCreature : MonoBehaviour {

    // reference to a HoverPreview Component
    private HoverPreview hover;
    public Draggable draggable;
    public DraggingActions draggingActions;
    // reference to a canvas on this object to set sorting order
    private Canvas canvas;
    private OneCardManager oneCardManager;
    // a value for canvas sorting order when we want to show this object above everything
    private int TopSortingOrder = 500;

    // PROPERTIES
    private int slot = -1;
    public int Slot
    {
        get{ return slot;}

        set
        {
            slot = value;
            /*if (value != -1)
            {
                canvas.sortingOrder = HandSortingOrder(slot);
            }*/
        }
    }

    private VisualStates state;
    public VisualStates VisualState
    {
        get{ return state; }  

        set
        {
            state = value;
            switch (state)
            {
                case VisualStates.LowHand:
                    hover.ThisPreviewEnabled = true;
                    draggable.enabled = true;
                    draggingActions.enabled = true;
                    break;
                case VisualStates.LowTable:
                    hover.ThisPreviewEnabled = true;
                    draggable.enabled = false;
                    draggingActions.enabled = false;

                    break;
                case VisualStates.TopTable:
                    hover.ThisPreviewEnabled = true;
                    draggable.enabled = false;
                    draggingActions.enabled = false;
 
                    break;
                case VisualStates.Transition:
                    hover.ThisPreviewEnabled = false;
                    draggable.enabled = false;
                    draggingActions.enabled = false;
                    break;
                case VisualStates.Dragging:
                    hover.ThisPreviewEnabled = false;
                    break;
                case VisualStates.TopHand:
                    draggable.enabled = true;
                    draggingActions.enabled = true;
                    hover.ThisPreviewEnabled = true;
                    break;
            }
        }
    }

    void Awake()
    {
        hover = GetComponent<HoverPreview>();
        // for characters hover is attached to a child game object
        if (hover == null)
            hover = GetComponentInChildren<HoverPreview>();
        canvas = GetComponentInChildren<Canvas>();
        oneCardManager= gameObject.GetComponent<OneCardManager>();
    }

    public void BringToFront()
    {
        canvas.sortingOrder = TopSortingOrder;
        canvas.sortingLayerName = "AboveEverything";
    }

    // not setting sorting order inside of VisualStaes property because when the card is drawn, 
    // we want to set an index first and set the sorting order only when the card arrives to hand. 
    public void SetHandSortingOrder()
    {
        if (slot != -1)
            canvas.sortingOrder = HandSortingOrder(slot);
        canvas.sortingLayerName = "Cards";
    }

    public void SetTableSortingOrder()
    {
        canvas.sortingOrder = 0;
        canvas.sortingLayerName = "Creatures";
    }

    private int HandSortingOrder(int placeInHand)
    {
        return (-(placeInHand + 1) * 10); 
    }


}
