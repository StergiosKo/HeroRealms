using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prepare", menuName = "ScriptableObjects/Effects/Prepare")]
public class Prepare : BaseEffect
{
    public override void Activate(Player player, int amount)
    {
        for(int i=0; i<amount; i++)
        {
            player.Prepare();
        }
    }
}
