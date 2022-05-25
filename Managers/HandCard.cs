using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCard : CardHolder
{
    public Player player;
    public bool discardable = false;

    public GameObject cardBack;
    private bool canBeHovered = false;

    public void PlayThis()
    {
        if (player.Turn() && !GameManager.someoneChoosing)
        {
            if (baseCard.IsSacrificable())
            {
                player.SacrificableCard(baseCard);
            }
            else
            {
                player.PlayCard(this.baseCard);
            }

        }
    }

    public void DiscardThisCard()
    {
        if (discardable)
        {
            player.ThisCardDiscard(this.baseCard);
        }

    }


    private void SacrificeCard()
    {
        gameObject.GetComponent<Sacrificable>().Clicked();
    }



    public void Clicked()
    {
        if (discardable) DiscardThisCard();
        else if (GameManager.chooseSacrifice && GameManager.currentPlayer == player) SacrificeCard();
        else PlayThis();
    }

    public override void Hover()
    {
        if (canBeHovered) base.Hover();
    }

    public void CanBeHovered(bool flag)
    {
        canBeHovered = flag;
        cardBack.gameObject.SetActive(!flag);
    }
}
