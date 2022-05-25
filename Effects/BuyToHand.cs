using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuyToHand", menuName = "ScriptableObjects/Effects/BuyToHand")]
public class BuyToHand : BuyToTop
{
    public override void Activate(Player player, int amount)
    {
        base.Activate(player, amount);
        player.returnToHand = true;
    }
}
