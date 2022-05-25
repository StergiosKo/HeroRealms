using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public Text countText;

    public int GetSize()
    {
        return cards.Count;
    }

    public void SetCount()
    {
        countText.text = GetSize().ToString();
    }

    public void Empty()
    {
        cards.Clear();
        SetCount();
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
        SetCount();
    }

    public void RemoveCard(Card card)
    {
        cards.Remove(card);
        SetCount();
    }


    public void SetDeck(List<Card> aDeck)
    {
        cards = new List<Card>();
        cards = aDeck;
        SetCount();
    }

    public Card Draw()
    {
        Card returnCard = cards[0];
        cards.RemoveAt(0);
        SetCount();
        return returnCard;
    }

    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Card temp = cards[i];
            int randomIndex = UnityEngine.Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    public void CopyDeck(Deck original)
    {
        this.Empty();
        foreach (Card card in original.cards)
        {
            this.AddCard(card);
        }
    }

    public void AddInTop(Card card)
    {
        cards.Insert(0, card);
        SetCount();
    }

}
