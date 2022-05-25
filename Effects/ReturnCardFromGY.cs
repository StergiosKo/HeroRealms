using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReturnCardFromGY", menuName = "ScriptableObjects/Effects/ReturnCard")]
public class ReturnCardFromGY : BaseEffect
{

    public List<CardType> cardTypes;

    public override void Activate(Player player, int amount)
    {
        player.ReturnCardFromGY(cardTypes);
    }
}
