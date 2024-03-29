﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Player : MonoBehaviour, ICharacter
{
    public int PlayerID;
    public Character charAsset;
    public PlayerArea PArea;
    public SpellEffect HeroPowerEffect;

    public Deck deck;
    public Hand hand;
    public Table table;

    public Button endTurnButton;

    public int bonusNumber;
    private int bonusManaThisTurn = 0;
    public bool usedHeroPowerThisTurn = false;

    public List<Command> commandsWaitinglist = new List<Command>();

    public int ID
    {
        get{ return PlayerID; }
    }

    private int manaThisTurn;
    public int ManaThisTurn
    {
        get{ return manaThisTurn;}
        set
        {
            manaThisTurn = value;
            //PArea.ManaBar.TotalCrystals = manaThisTurn;
            new UpdateManaCrystalsCommand(this, manaThisTurn, manaLeft).AddToQueue();
        }
    }

    private int manaLeft;
    public int ManaLeft
    {
        get
        { return manaLeft;}
        set
        {
            manaLeft = value;
            //PArea.ManaBar.AvailableCrystals = manaLeft;
            new UpdateManaCrystalsCommand(this, ManaThisTurn, manaLeft).AddToQueue();
            //Debug.Log(ManaLeft);
            if (TurnManager.Instance.whoseTurn == this)
                HighlightPlayableCards();
        }
    }

    public Player otherPlayer
    {
        get
        {
            if (Players[0] == this)
                return Players[1];
            else
                return Players[0];
        }
    }

    private int health;
    public int Health
    {
        get { return health;}
        set
        {
            health = value; 
        }
    }

    public delegate void VoidWithNoArguments();
    //public event VoidWithNoArguments CreaturePlayedEvent;
    //public event VoidWithNoArguments SpellPlayedEvent;
    //public event VoidWithNoArguments StartTurnEvent;
    public event VoidWithNoArguments EndTurnEvent;

    public static Player[] Players;

    void Awake()
    {
        Players = GameObject.FindObjectsOfType<Player>();
        PlayerID = IDFactory.GetUniqueID();
    }

    /*void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            DrawACard();
        }
    }*/


    public virtual void OnTurnStart()
    {
        // add one mana crystal to the pool;
        //Debug.Log("In ONTURNSTART for "+ gameObject.name);
        //usedHeroPowerThisTurn = false;
        //ManaThisTurn++;
        //ManaLeft = ManaThisTurn;
        //foreach (CardLogic cl in table.CardsOnTable)
        //cl.OnTurnStart();
        //PArea.HeroPower.WasUsedThisTurn = false;
        endTurnButton.interactable = true;

    }

    public void GetBonusMana(int amount)
    {
        bonusManaThisTurn += amount;
        ManaThisTurn += amount;
        ManaLeft += amount;
    }   

    public void OnTurnEnd(int dicenum)
    {

        if (EndTurnEvent != null)
            EndTurnEvent.Invoke();


        //ManaThisTurn -= bonusManaThisTurn;
        //bonusManaThisTurn = 0;
        GetComponent<TurnMaker>().StopAllCoroutines();
    }

    public void DrawACard(bool fast = false)
    {
        if (deck.cards.Count > 0)
        {
            if (hand.CardsInHand.Count < PArea.handVisual.slots.Children.Length)
            {
                // 1) save index to place a visual card into visual hand
                int indexToPlaceACard = hand.CardsInHand.Count;
                // 2) logic: add card to hand
                CardLogic newCard = new CardLogic(deck.cards[0]);
                newCard.owner = this;
                hand.CardsInHand.Add(newCard);
                // Debug.Log(hand.CardsInHand.Count);
                // 3) logic: remove the card from the deck
                deck.cards.RemoveAt(0);
                // 4) create a command
                deck.nextCards.Add(newCard.ca);
                new DrawACardCommand(hand.CardsInHand[indexToPlaceACard], this, indexToPlaceACard, fast, fromDeck: true).AddToQueue(); 
            }
        }
        else
        {
            deck.cards = new List<Cards>(deck.nextCards);
            deck.nextCards.Clear();
            deck.cards.Shuffle();
            DrawACard();
        }
       
    }

    public void DrawACoin()
    {
        if (hand.CardsInHand.Count < PArea.handVisual.slots.Children.Length)
        {
            // 1) logic: add card to hand
            CardLogic newCard = new CardLogic(GlobalSettings.Instance.CoinCard);
            newCard.owner = this;
            hand.CardsInHand.Add(newCard);
            // 2) send message to the visual Deck
            new DrawACardCommand(hand.CardsInHand[hand.CardsInHand.Count - 1], this, hand.CardsInHand.Count - 1, fast: true, fromDeck: false).AddToQueue(); 
        }
        // no removal from deck because the coin was not in the deck
    }

    public void PlayASpellFromHand(int SpellCardUniqueID, int TargetUniqueID)
    {
        // TODO: !!!
        // if TargetUnique ID < 0 , for example = -1, there is no target.
        if (TargetUniqueID < 0)
            PlayASpellFromHand(CardLogic.CardsCreatedThisGame[SpellCardUniqueID], null);
        else if (TargetUniqueID == ID)
        {
            PlayASpellFromHand(CardLogic.CardsCreatedThisGame[SpellCardUniqueID], this);
        }
        else if (TargetUniqueID == otherPlayer.ID)
        {
            PlayASpellFromHand(CardLogic.CardsCreatedThisGame[SpellCardUniqueID], this.otherPlayer);
        }
        else
        {
            // target is a creature
            PlayASpellFromHand(CardLogic.CardsCreatedThisGame[SpellCardUniqueID], CreatureLogic.CreaturesCreatedThisGame[TargetUniqueID]);
        }

    }

    public void PlayASpellFromHand(CardLogic playedCard, ICharacter target)
    {
        //ManaLeft -= playedCard.CurrentManaCost;
        // cause effect instantly:
        commandsWaitinglist.Add(new TriggerEffectCommand(playedCard, target, this, table));
        // no matter what happens, move this card to PlayACardSpot
        new PlayASpellCardCommand(this,playedCard).AddToQueue();
        // remove this card from hand
        hand.CardsInHand.Remove(playedCard);
        table.CardsOnTable.Add(playedCard);
        // check if this is a creature or a spell
    }

    public void PlayACreatureFromHand(int UniqueID, int tablePos)
    {
        PlayACreatureFromHand(CardLogic.CardsCreatedThisGame[UniqueID], tablePos);
    }

    public void PlayACreatureFromHand(CardLogic playedCard, int tablePos)
    {
       /* Debug.Log(ManaLeft);
        Debug.Log(playedCard.CurrentManaCost);
        ManaLeft -= playedCard.CurrentManaCost;
        Debug.Log("Mana Left after played a creature: " + ManaLeft);
        // create a new creature object and add it to Table
        CreatureLogic newCreature = new CreatureLogic(this, playedCard.ca);
        table.CreaturesOnTable.Insert(tablePos, newCreature);
        // no matter what happens, move this card to PlayACardSpot
        new PlayACreatureCommand(playedCard, this, tablePos, newCreature.UniqueCreatureID).AddToQueue();
        // remove this card from hand
        hand.CardsInHand.Remove(playedCard);
        HighlightPlayableCards();
        */
    }

    /*
     public void PlayACardFromHand(int UniqueID, int tablePos)
    {
        PlayACardFromHand(CardLogic.CardsCreatedThisGame[UniqueID], tablePos);
    }

    public void PlayACardFromHand(CardLogic playedCard, int tablePos)
    {
        Debug.Log(ManaLeft);
         Debug.Log(playedCard.CurrentManaCost);
         ManaLeft -= playedCard.CurrentManaCost;
         Debug.Log("Mana Left after played a creature: " + ManaLeft);
         // create a new creature object and add it to Table
         CreatureLogic newCreature = new CreatureLogic(this, playedCard.ca);
         table.CreaturesOnTable.Insert(tablePos, newCreature);
         // no matter what happens, move this card to PlayACardSpot
         new PlayACreatureCommand(playedCard, this, tablePos, newCreature.UniqueCreatureID).AddToQueue();
         // remove this card from hand
         hand.CardsInHand.Remove(playedCard);
         HighlightPlayableCards();
        

        CardLogic newCard = new CardLogic(playedCard.ca);
        table.CardsOnTable.Insert(tablePos, newCard);
        new PlayASpellCardCommand(playedCard, this, tablePos, newCard.UniqueCardID).AddToQueue();
        hand.CardsInHand.Remove(playedCard);
        HighlightPlayableCards();
    }
    */

    public void Die()
    {
        // game over
        // block both players from taking new moves 
        PArea.ControlsON = false;
        otherPlayer.PArea.ControlsON = false;
        //TurnManager.Instance.StopTheTimer();
        PArea.Portrait.Explode();
    }

    // METHODS TO SHOW GLOW HIGHLIGHTS
    public void HighlightPlayableCards(bool removeAllHighlights = false)
    {
        //Debug.Log("HighlightPlayable remove: "+ removeAllHighlights);
        foreach (CardLogic cl in hand.CardsInHand)
        {
            GameObject g = IDHolder.GetGameObjectWithID(cl.UniqueCardID);
            if (g!=null)
                g.GetComponent<OneCardManager>().CanBePlayedNow = (cl.CurrentManaCost <= ManaLeft) && !removeAllHighlights;
        }

            this.PArea.Portrait.PortraitGlowImage.enabled = !removeAllHighlights;

        /*foreach (CardLogic cdl in table.CardsOnTable)
        {
            GameObject g = IDHolder.GetGameObjectWithID(cdl.UniqueCardID);
            if (g != null)
                g.GetComponent<OneCardManager>().CanBePlayedNow = (cdl.CurrentManaCost <= ManaLeft) && !removeAllHighlights;
        }*/
            
        // highlight hero power
        //PArea.HeroPower.Highlighted = (!usedHeroPowerThisTurn) && (ManaLeft > 1) && !removeAllHighlights;
    }

    // START GAME METHODS
    public void LoadCharacterInfoFromAsset()
    {
        Health = charAsset.MaxHealth;
        // change the visuals for portrait, hero power, etc...
        PArea.Portrait.charAsset = charAsset;
        PArea.Portrait.ApplyLookFromAsset();
        // TODO: insert the code to attach hero power script here. 
       /* if (charAsset.HeroPowerName != null && charAsset.HeroPowerName != "")
        {
            HeroPowerEffect = System.Activator.CreateInstance(System.Type.GetType(charAsset.HeroPowerName)) as SpellEffect;
        }
        else
        {
            Debug.LogWarning("Check hero powr name for character " + charAsset.ClassName);
        }*/
    }

    public void TransmitInfoAboutPlayerToVisual()
    {
        PArea.Portrait.gameObject.AddComponent<IDHolder>().UniqueID = PlayerID;
        if (GetComponent<TurnMaker>() is AITurnMaker)
        {
            // turn off turn making for this character
            PArea.AllowedToControlThisPlayer = false;
        }
        else
        {
            // allow turn making for this character
            PArea.AllowedToControlThisPlayer = true;
        }
    }

    public void UseHeroPower()
    {
        ManaLeft -= 2;
        usedHeroPowerThisTurn = true;
        HeroPowerEffect.ActivateEffect();
    }
}
