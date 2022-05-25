using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
    public Card baseCard;
    public CardSO cardSO;
    public Image artwork;

    private void Start()
    {
        if (cardSO != null) UpdateCard();
    }

    public void SetCard(Card newCard)
    {
        baseCard = newCard;
        cardSO = baseCard.cardSO;
        UpdateCard();
    }

    public void SetCard(CardSO cardSO)
    {
        this.cardSO = cardSO;
        UpdateCard();
    }

    public virtual void UpdateCard()
    {
        artwork.sprite = cardSO.artwork;
    }

    public virtual void Hover()
    {
        GameObject.Find("ZoomCard").GetComponent<CardHolder>().SetCard(baseCard);
    }

}
