using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Spell, Weapon }
public enum SpellType { Damage, Heal, Complex }
public enum TargetingOptions {NoTarget, Enemy, Self, AllCharacters, EnemyHand, Hand, Deck, DiscardPile }
[CreateAssetMenu(fileName ="New Card",menuName = "Card")]
public class Cards : ScriptableObject
{
    [Header("General info")]
    public Character character;
    public CardType cardType;
    [TextArea(2, 3)]
    public string description;
    public Sprite cardImage;
    
    [Header("Bonus NumberInfo")]
    public int[] bonusNumberArray;
    public int bonusAmount;
    public int nonebonusAmount;
    public int bonusAttack;
    public int nonebonusAttack;

    [Header("Spell info")]
    public SpellType spellType;
    public int basicAmount=0;
    [HideInInspector]
    public int actAmount;
    public TargetingOptions targets;
    public int specialSpellAmount = 1;
    public string SpellScriptName;

    [Header("Weapon info")]
    public int basicAttack=0;
    [HideInInspector]
    public int actAttack;
    public int attacksForOneTurn = 1;
}
