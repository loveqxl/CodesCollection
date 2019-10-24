using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class TableVisual : MonoBehaviour 
{
    // PUBLIC FIELDS

    // an enum that mark to whish caracter this table belongs. The alues are - Top or Low
    public AreaPosition owner;

    // a referense to a game object that marks positions where we should put new Creatures
    public SameDistanceChildren slots;

    public Transform PlayPreviewSpot;
    // PRIVATE FIELDS

    // list of all the cards on the table as GameObjects
    private List<GameObject> CardsOnTable = new List<GameObject>();

    // are we hovering over this table`s collider with a mouse
    private bool cursorOverThisTable = false;

    // A 3D collider attached to this game object
    private BoxCollider col;

    // PROPERTIES

    public void AddCard(GameObject card)
    {

        // we allways insert a new card as 0th element in CardsInHand List 
        CardsOnTable.Insert(0, card);

        // parent this card to our Slots GameObject
        card.transform.SetParent(slots.transform);

        // re-calculate the position of the hand
        PlaceCardsOnNewSlots();
        UpdatePlacementOfSlots();
    }

    public void PutACardOnTable(GameObject card,int UniqueID) {
        // Set a tag to reflect where this card is
        foreach (Transform t in card.GetComponentsInChildren<Transform>())
            t.tag = owner.ToString() + "Card";
        // pass this card to tableVisual class
        AddCard(card);

        // Bring card to front while it travels from draw spot to hand
        WhereIsTheCardOrCreature w = card.GetComponent<WhereIsTheCardOrCreature>();
        w.BringToFront();
        w.Slot = 0;
        w.VisualState = VisualStates.Transition;


        // move card to the hand;
        Sequence s = DOTween.Sequence();

            // displace the card so that we can select it in the scene easier.
            s.Append(card.transform.DOLocalMove(slots.Children[0].transform.localPosition, GlobalSettings.Instance.CardTransitionTimeFast));
            s.Insert(0f, card.transform.DORotate(Vector3.zero, GlobalSettings.Instance.CardTransitionTimeFast));
   
        s.OnComplete(() => ChangeLastCardStatusToOnTable(card, w));
    }

    // this method will be called when the card arrived to table 
    void ChangeLastCardStatusToOnTable(GameObject card, WhereIsTheCardOrCreature w)
    {
        //Debug.Log("Changing state to Hand for card: " + card.gameObject.name);
        if (owner == AreaPosition.Low)
            w.VisualState = VisualStates.LowTable;
        else
            w.VisualState = VisualStates.TopTable;

        // set correct sorting order
        w.SetHandSortingOrder();
        // end command execution for DrawACArdCommand
        Command.CommandExecutionComplete();
    }

    void UpdatePlacementOfSlots()
    {
        float posX;
        if (CardsOnTable.Count > 0)
            posX = (slots.Children[0].transform.localPosition.x - slots.Children[CardsOnTable.Count - 1].transform.localPosition.x) / 2f;
        else
            posX = 0f;

        // tween Slots GameObject to new position in 0.3 seconds
        slots.gameObject.transform.DOLocalMoveX(posX, 0.3f);
    }

    // returns true if we are hovering over any player`s table collider
    public static bool CursorOverSomeTable
    {
        get
        {
            TableVisual[] bothTables = GameObject.FindObjectsOfType<TableVisual>();
            return (bothTables[0].CursorOverThisTable || bothTables[1].CursorOverThisTable);
        }
    }

    // returns true only if we are hovering over this table`s collider
    public bool CursorOverThisTable
    {
        get{ return cursorOverThisTable; }
    }

    // METHODS

    // MONOBEHAVIOUR SCRIPTS (mouse over collider detection)
    void Awake()
    {
        col = GetComponent<BoxCollider>();
    }

    // CURSOR/MOUSE DETECTION
    void Update()
    {
        // we need to Raycast because OnMouseEnter, etc reacts to colliders on cards and cards "cover" the table
        // create an array of RaycastHits
        RaycastHit[] hits;
        // raycst to mousePosition and store all the hits in the array
        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 30f);

        bool passedThroughTableCollider = false;
        foreach (RaycastHit h in hits)
        {
            // check if the collider that we hit is the collider on this GameObject
            if (h.collider == col)
                passedThroughTableCollider = true;
        }
        cursorOverThisTable = passedThroughTableCollider;
    }
   
    // method to create a new creature and add it to the table
    public void AddCreatureAtIndex(Cards ca, int UniqueID ,int index)
    {
        // create a new creature from prefab
        GameObject creature = GameObject.Instantiate(GlobalSettings.Instance.CreaturePrefab, slots.Children[index].transform.position, Quaternion.identity) as GameObject;

        // apply the look from CardAsset
        OneCreatureManager manager = creature.GetComponent<OneCreatureManager>();
        manager.cardAsset = ca;
        manager.ReadCreatureFromAsset();

        // add tag according to owner
        foreach (Transform t in creature.GetComponentsInChildren<Transform>())
            t.tag = owner.ToString()+"Creature";
        
        // parent a new creature gameObject to table slots
        creature.transform.SetParent(slots.transform);

        // add a new creature to the list
        CardsOnTable.Insert(index, creature);

        // let this creature know about its position
        WhereIsTheCardOrCreature w = creature.GetComponent<WhereIsTheCardOrCreature>();
        w.Slot = index;
        w.VisualState = VisualStates.LowTable;

        // add our unique ID to this creature
        IDHolder id = creature.AddComponent<IDHolder>();
        id.UniqueID = UniqueID;

        // after a new creature is added update placing of all the other creatures
        ShiftSlotsGameObjectAccordingToNumberOfCards();
        PlaceCardsOnNewSlots();

        // end command execution
        Command.CommandExecutionComplete();
    }


    // returns an index for a new creature based on mousePosition
    // included for placing a new creature to any positon on the table
    public int TablePosForNewCreature(float MouseX)
    {
        // if there are no creatures or if we are pointing to the right of all creatures with a mouse.
        // right - because the table slots are flipped and 0 is on the right side.
        if (CardsOnTable.Count == 0 || MouseX > slots.Children[0].transform.position.x)
            return 0;
        else if (MouseX < slots.Children[CardsOnTable.Count - 1].transform.position.x) // cursor on the left relative to all creatures on the table
            return CardsOnTable.Count;
        for (int i = 0; i < CardsOnTable.Count; i++)
        {
            if (MouseX < slots.Children[i].transform.position.x && MouseX > slots.Children[i + 1].transform.position.x)
                return i + 1;
        }
        Debug.Log("Suspicious behavior. Reached end of TablePosForNewCreature method. Returning 0");
        return 0;
    }

    // method to create a new card and add it to the table
    public void AddSpellCardAtIndex(Cards ca, int UniqueID, int index,CardLogic cl,Player p,ICharacter target)
    {
        GameObject card;
        // create a new creature from prefab
        if (target == null)
        {
            card = GameObject.Instantiate(GlobalSettings.Instance.NoTargetSpellCardPrefab, slots.Children[index].transform.position, Quaternion.identity) as GameObject;
            card.GetComponent<Draggable>().enabled = false;// just can't move when drag should somehow set "Candrag" in DraggingAction to false;
        }
        else {
            card = GameObject.Instantiate(GlobalSettings.Instance.TargetedSpellCardPrefab, slots.Children[index].transform.position, Quaternion.identity) as GameObject;
            card.GetComponent<Draggable>().enabled = false;// just can't move when drag should somehow set "Candrag" in DraggingAction to false;
        }
       


        // apply the look from CardAsset
        OneCardManager manager = card.GetComponent<OneCardManager>();
        manager.cardAsset = ca;
        manager.ReadCardFromAsset();

        // add tag according to owner
        foreach (Transform t in card.GetComponentsInChildren<Transform>())
            t.tag = owner.ToString() + "Card";

        // parent a new creature gameObject to table slots
        card.transform.SetParent(slots.transform);

        // add a new creature to the list
        CardsOnTable.Insert(index, card);

        // let this Card know about its position
        WhereIsTheCardOrCreature w = card.GetComponent<WhereIsTheCardOrCreature>();
        w.Slot = index;
        w.VisualState = VisualStates.LowTable;

        // add our unique ID to this Card
        IDHolder id = card.AddComponent<IDHolder>();
        id.UniqueID = UniqueID;

        // after a new creature is added update placing of all the other creatures
        ShiftSlotsGameObjectAccordingToNumberOfCards();
        PlaceCardsOnNewSlots();

        p.commandsWaitinglist.Add(new TriggerEffectCommand(cl, target, p, p.table));

        // end command execution
        Command.CommandExecutionComplete();
    }


    // returns an index for a new creature based on mousePosition
    // included for placing a new creature to any positon on the table
    public int TablePosForNewCard(float MouseX)
    {
        // if there are no creatures or if we are pointing to the right of all creatures with a mouse.
        // right - because the table slots are flipped and 0 is on the right side.
        if (CardsOnTable.Count == 0 || MouseX > slots.Children[0].transform.position.x)
            return 0;
        else if (MouseX < slots.Children[CardsOnTable.Count - 1].transform.position.x) // cursor on the left relative to all creatures on the table
            return CardsOnTable.Count;
        for (int i = 0; i < CardsOnTable.Count; i++)
        {
            if (MouseX < slots.Children[i].transform.position.x && MouseX > slots.Children[i + 1].transform.position.x)
                return i + 1;
        }
        Debug.Log("Suspicious behavior. Reached end of TablePosForNewCreature method. Returning 0");
        return 0;
    }

    // Destroy a creature
    public void RemoveCreatureWithID(int IDToRemove)
    {
        // TODO: This has to last for some time
        // Adding delay here did not work because it shows one creature die, then another creature die. 
        // 
        //Sequence s = DOTween.Sequence();
        //s.AppendInterval(1f);
        //s.OnComplete(() =>
        //   {
                
        //    });
        GameObject creatureToRemove = IDHolder.GetGameObjectWithID(IDToRemove);
        CardsOnTable.Remove(creatureToRemove);
        Destroy(creatureToRemove);

        ShiftSlotsGameObjectAccordingToNumberOfCards();
        PlaceCardsOnNewSlots();
        Command.CommandExecutionComplete();
    }

    /// <summary>
    /// Shifts the slots game object according to number of creatures.
    /// </summary>
    void ShiftSlotsGameObjectAccordingToNumberOfCards()
    {
        float posX;
        if (CardsOnTable.Count > 0)
            posX = (slots.Children[0].transform.localPosition.x - slots.Children[CardsOnTable.Count - 1].transform.localPosition.x) / 2f;
        else
            posX = 0f;

        slots.gameObject.transform.DOLocalMoveX(posX, 0.3f);  
    }

    /// <summary>
    /// After a new creature is added or an old creature dies, this method
    /// shifts all the creatures and places the creatures on new slots.
    /// </summary>
    void PlaceCardsOnNewSlots()
    {
        foreach (GameObject g in CardsOnTable)
        {
            g.transform.DOLocalMoveX(slots.Children[CardsOnTable.IndexOf(g)].transform.localPosition.x, 0.3f);
            // apply correct sorting order and HandSlot value for later 
            // TODO: figure out if I need to do something here:
            // g.GetComponent<WhereIsTheCardOrCreature>().SetTableSortingOrder() = CreaturesOnTable.IndexOf(g);
            WhereIsTheCardOrCreature w = g.GetComponent<WhereIsTheCardOrCreature>();
            w.Slot = CardsOnTable.IndexOf(g);
            w.SetHandSortingOrder();
        }
    }

    public void RemoveCard(GameObject card)
    {
        // remove a card from the list
        CardsOnTable.Remove(card);

        // re-calculate the position of the hand
        PlaceCardsOnNewSlots();
        ShiftSlotsGameObjectAccordingToNumberOfCards();
    }

    public void PlayASpellFromTable(int CardID)
    {
        GameObject card = IDHolder.GetGameObjectWithID(CardID);
        PlayASpellFromTable(card);
    }

    public void PlayASpellFromTable(GameObject CardVisual)
    {
        
        CardVisual.GetComponent<WhereIsTheCardOrCreature>().VisualState = VisualStates.Transition;
        RemoveCard(CardVisual);

        CardVisual.transform.SetParent(null);

        Sequence s = DOTween.Sequence();
        s.Append(CardVisual.transform.DOMove(PlayPreviewSpot.position, 0.5f));
        s.Insert(0f, CardVisual.transform.DORotate(Vector3.zero, 0.5f));
        s.AppendInterval(1f);
        s.OnComplete(() =>
        {
            Destroy(CardVisual);
            Command.CommandExecutionComplete();
            
        });
    }

}
