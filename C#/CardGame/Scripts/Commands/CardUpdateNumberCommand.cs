using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUpdateNumberCommand : Command
{
    private CardLogic cardLogic;
    private int bonusNumber;

    public CardUpdateNumberCommand(CardLogic cardLogic,int bonusNumber) {
        this.cardLogic = cardLogic;
        this.bonusNumber = bonusNumber;

    }

    public override void StartCommandExecution()
    {
        GameObject card = IDHolder.GetGameObjectWithID(cardLogic.ID);
        if (System.Array.IndexOf(cardLogic.ca.bonusNumberArray, bonusNumber) > -1)
        {
            //bonus
            if (cardLogic.ca.cardType == CardType.Spell)
            {
                cardLogic.ca.actAmount = cardLogic.ca.bonusAmount;
                cardLogic.actAmount = cardLogic.ca.actAmount;
                card.GetComponent<OneCardManager>().ShowNumberChange(cardLogic.ca.bonusAmount);
            }
            else if (cardLogic.ca.cardType == CardType.Weapon)
            {
                cardLogic.ca.actAttack = cardLogic.ca.bonusAttack;
                cardLogic.actAmount = cardLogic.ca.actAttack;
                card.GetComponent<OneCardManager>().ShowNumberChange(cardLogic.ca.bonusAttack);
            }
        }
        else {
            //normal
            if (cardLogic.ca.cardType == CardType.Spell)
            {
                cardLogic.ca.actAmount = cardLogic.ca.nonebonusAmount;
                cardLogic.actAmount = cardLogic.ca.actAmount;
                card.GetComponent<OneCardManager>().ShowNumberChange(cardLogic.ca.nonebonusAmount);
            }
            else if (cardLogic.ca.cardType == CardType.Weapon)
            {
                cardLogic.ca.actAttack = cardLogic.ca.nonebonusAttack;
                cardLogic.actAmount = cardLogic.ca.actAttack;
                card.GetComponent<OneCardManager>().ShowNumberChange(cardLogic.ca.nonebonusAttack);
            }
        }
    }

}
