using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Returnable : MonoBehaviour
{
    public Player player;

    public void Clicked()
    {
        if (GameManager.returnCardDeck && GameManager.currentPlayer == player)
        {
            Card card = gameObject.GetComponent<CardHolder>().baseCard;
            if (GameManager.cardTypes.Contains(card.cardSO.cardType)) player.CardReturnedFromGY(card);
        }
    }
}
