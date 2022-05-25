using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CardManager : MonoBehaviour
{
    [Serializable]
    public struct CardInfo
    {
        public CardSO cardSO;
        public int amount;
    }

    public List<CardInfo> baseDeck = new List<CardInfo>();
    public List<CardInfo> buyDeckTemplate = new List<CardInfo>();
    public CardSO currencyCard;


    public List<Card> GetBuyDeck()
    {
        List<Card> deck = new List<Card>();  
        foreach (CardInfo cardInfo in buyDeckTemplate)
        {
            for (int i = 0; i < cardInfo.amount; i++)
            {
                deck.Add(CreateCard(cardInfo.cardSO));
            }
        }
        return deck;
    }

    private Card CreateCard(CardSO cardSO)
    {
        Card newCard;
        if (cardSO.cardType == CardType.Unit)
        {
            newCard = new UnitCard(cardSO);
        }
        else if (cardSO.cardType == CardType.Spell)
        {
            newCard = new SpellCard(cardSO);
        }
        else
        {
            newCard = new CurrencyCard(cardSO);
        }
        return newCard;
    }

    public void GiveDeck(Player player)
    {
        foreach(CardInfo cardInfo in baseDeck)
        {
            for(int i=0; i<cardInfo.amount; i++)
            {
                Card newCard = CreateCard(cardInfo.cardSO);
               player.AddCardDeck(newCard);
            }
        }
    }

    public Card GiveCurrency()
    {
        return new CurrencyCard(currencyCard);
    }
}