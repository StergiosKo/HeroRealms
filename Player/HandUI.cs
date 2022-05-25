using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUI : MonoBehaviour
{

    public List<CardHolder> holders;

    public Hand hand;
    public Player player;

    public void NewHand(Hand newHand)
    {
        hand = newHand;
        UpdateHand();
    }

    private void UpdateHand()
    {
        RemoveHand();
        for(int i=0; i<hand.currentCards; i++)
        {
            holders[i].gameObject.SetActive(true);
            holders[i].SetCard(hand.cards[i]);
        }
    }

    private void RemoveHand()
    {
        foreach (CardHolder holder in holders)
        {
            holder.gameObject.SetActive(false);
        }
    }
}
