using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacrificable : MonoBehaviour
{

    public Player player;

    public void Clicked()
    {
        if (GameManager.chooseSacrifice && GameManager.currentPlayer == player)
        {
            Card card = gameObject.GetComponent<CardHolder>().baseCard;
            if (player.IsCardInGraveyard(card))
            {
                player.SacrificeCardGY(card);
            }
            else
            {
                player.SacrificeCardHand(card);
            }
        }
    }
}
