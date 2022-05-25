using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiscardCard", menuName = "ScriptableObjects/Effects/DiscardCard")]
public class DiscardCard : BaseEffect
{
    public override void Activate(Player player, int amount)
    {
        for(int i=0; i<amount; i++)
        {
            player.DiscardCard();
        }
    }

}
