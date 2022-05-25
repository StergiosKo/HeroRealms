using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuyToTop", menuName = "ScriptableObjects/Effects/BuyToTop")]
public class BuyToTop : BaseEffect
{

    public List<CardType> cardTypes;

    public override void Activate(Player player, int amount)
    {
        foreach(CardType cardType in cardTypes)
        {
            player.AddCardTypeToTop(cardType);
        }
    }

}
