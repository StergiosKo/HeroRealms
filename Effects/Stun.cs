using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stun", menuName = "ScriptableObjects/Effects/Stun")]
public class Stun : BaseEffect
{
    public override void Activate(Player player, int amount)
    {
        for(int i=0; i<amount; i++)
        {
            player.StunUnit();
        }
    }

}
