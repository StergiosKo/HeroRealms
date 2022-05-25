using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum Color
{
    White,
    Green,
    Blue,
    Red,
    None
}

public enum CardType
{
    Unit,
    Spell,
    Currency
}

[Serializable]
public class Card
{

    public CardSO cardSO;

    protected string name;
    protected Color color;
    protected int cost;
    protected CardType cardType;

    public Card(CardSO cardSO)
    {
        this.cardSO = cardSO;
        UpdateCard();
    }

    public virtual void UpdateCard()
    {
        name = cardSO.name;
        color = cardSO.color;
        cost = cardSO.cost;
        cardType = cardSO.cardType;
    }

    public string GetName()
    {
        return name;
    }

    public virtual void Play(Player player)
    {
        UseEffects(player);
        player.AddCardGY(this);

    }


    public int GetCost()
    {
        return cost;
    }

    public virtual void UseEffects(Player player)
    {
        foreach (AnEffect effect in cardSO.effects)
        {
            effect.Activate(player);
        }
    }

    public bool IsSacrificable()
    {
        foreach (AnEffect effect in cardSO.effects)
        {
            if (effect.cost is SacrificeCost) return true;
        }
        return false;
    }


}
