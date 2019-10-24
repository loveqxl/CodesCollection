using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour {

    public List<Cards> cards = new List<Cards>();
    public List<Cards> nextCards = new List<Cards>();
    void Awake()
    {
        cards.Shuffle();
    }
	
}
