using UnityEngine;
using System.Collections;
using DG.Tweening;

// this class should be attached to the deck
// generates new cards and places them into the hand
public class PlayerDeckVisual : MonoBehaviour {
    public Deck deck;
    // public AreaPosition owner;
    public float HeightOfOneCard = 0.0012f;
    public GameObject deckCube;

    void Start()
    {
       CardsInDeck = deck.cards.Count;
    }

    private int cardsInDeck = 0;
    public int CardsInDeck
    {
        get{ return cardsInDeck; }

        set
        {
            cardsInDeck = value;
            transform.position = new Vector3(transform.position.x, transform.position.y, - HeightOfOneCard * value);
            deckCube.transform.localScale = new Vector3(deckCube.transform.localScale.x, deckCube.transform.localScale.y, CardsInDeck*HeightOfOneCard * value);
            deckCube.transform.localPosition = new Vector3(0, 0, CardsInDeck * HeightOfOneCard * value / 2 + 0.1f);
        }
    }
   
}
