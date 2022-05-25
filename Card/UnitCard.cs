using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCard : Card
{

    public int health;
    public bool guard;

    public bool activated;

    public UnitCard(CardSO cardSO) : base(cardSO)
    {
        UnitCardSO unit = (UnitCardSO)cardSO;
        health = unit.health;
        guard = unit.guard;
    }

    override public void UpdateCard()
    {
        UnitCardSO card = (UnitCardSO)cardSO;
        health = card.health;
        guard = card.guard;
        base.UpdateCard();
    }

    public override void Play(Player player)
    {
        player.PlayUnit(this);
        activated = false;
    }

    public override void UseEffects(Player player)
    {
        if (!activated)
        {
            base.UseEffects(player);
            activated = true;
        }

    }

    public void Ready()
    {
        activated = false;
    }

    public void Unready()
    {
        activated = true;
    }
}
