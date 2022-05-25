using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlots : CardHolder
{
    public Text cost;
    public BuyDeckHandler handler;

    public GameObject boughtAnimationObject;

    public override void UpdateCard()
    {
        base.UpdateCard();
        cost.text = cardSO.cost.ToString();
    }

    public void Clicked()
    {
        handler.BuyCard(this);
    }

    public Card GetCard()
    {
        return baseCard;
    }

    public void CardBought(Card newCard)
    {
        StartCoroutine(CardBoughtCoroutine(newCard));
    }

    private IEnumerator CardBoughtCoroutine(Card newCard)
    {
        yield return StartCoroutine(BoughtAnimation());
        yield return StartCoroutine(SetNewCard(newCard));
    }

    private IEnumerator BoughtAnimation()
    {
        boughtAnimationObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        boughtAnimationObject.SetActive(false);
    }

    private IEnumerator SetNewCard(Card card)
    {
        SetCard(card);
        yield return null;
    }
}
