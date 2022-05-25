using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    public List<Card> cards;

    public int currentCards = 0;
    public int maxCards;

    public List<HandCard> holders;

    public void AddCard(Card card)
    {
        if (currentCards < maxCards)
        {
            cards.Add(card);
            currentCards++;
        }
    }

    public void UpdateHand()
    {
        RemoveHand();
        for (int i = 0; i < currentCards; i++)
        {
            holders[i].gameObject.SetActive(true);
            holders[i].SetCard(cards[i]);
        }
    }

    private void RemoveHand()
    {
        foreach (HandCard holder in holders)
        {
            holder.gameObject.SetActive(false);
        }
    }

    public bool Limit()
    {
        if (currentCards == maxCards) return true;
        else return false;
    }

    public void RemoveCard(Card card)
    {
        cards.Remove(card);
        currentCards--;
        UpdateHand();
    }

    public void RemoveHand(Player player)
    {
        for (int i = 0; i < currentCards; i++)
        {
            player.AddCardGY(holders[i].baseCard);
        }
        cards.Clear();
        currentCards = 0;
        UpdateHand();
    }

    public void CardsDiscardable(bool flag)
    {
        for(int i=0; i < currentCards; i++)
        {
            holders[i].discardable = flag;
        }
    }

    public bool IsEmpty()
    {
        if (currentCards == 0) return true;
        else return false;
    }

    public List<Color> GetColorAffinity()
    {
        List<Color> colorList = new List<Color>();
        for (int i = 0; i < currentCards; i++)
        {
            colorList.Add(holders[i].cardSO.color);
        }
        return colorList;
    }

    public List<CardHolder> GetCards()
    {
        List<CardHolder> availableCards = new List<CardHolder>();
        for(int i=0; i < currentCards; i++)
        {
            availableCards.Add(holders[i]);
        }
        return availableCards;
    }

    public void CanBeSeen(bool flag)
    {
        foreach(HandCard card in holders)
        {
            card.CanBeHovered(flag);
        }
    }

}
