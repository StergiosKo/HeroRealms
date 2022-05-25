using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyDeckHandler : MonoBehaviour
{

    public Deck buyDeck;
    public CardManager manager;
    public Player currentPlayer;

    public List<CardSlots> slots;
    public CardSlots currencySlot;

    public Text buyDeckCount;

    private void Start()
    {
        NewDeck();
        buyDeck.Shuffle();
        foreach(CardHolder slot in slots)
        {
            slot.SetCard(GetNextBuyCard());
        }
        currencySlot.SetCard(manager.GiveCurrency());
    }

    public void NewDeck()
    {
        buyDeck.SetDeck(manager.GetBuyDeck());
    }

    public Card GetNextBuyCard()
    {
        if (buyDeck.GetSize() == 0) NewDeck();
        return buyDeck.Draw();
    }

    public void BuyCard(CardSlots slot)
    {
        if (currentPlayer.TryBuyCard(slot.baseCard))
        {
            slot.CardBought(GetNextBuyCard());
        }
    }

    public void BuyCurrency()
    {
        if (currentPlayer.TryBuyCard(currencySlot.baseCard))
        {
            currencySlot.CardBought(manager.GiveCurrency());
        }
    }

    public CardSlots HighestCostCard(int gold)
    {
        CardSlots returnCard = null;
        int maxCost = 0;
        foreach(CardSlots slot in slots)
        {
            if (slot.baseCard.cardSO.cost > maxCost && slot.baseCard.cardSO.cost <= gold)
            {
                returnCard = slot;
                maxCost = slot.baseCard.cardSO.cost;
            }
        }
        return returnCard;
    }


}
