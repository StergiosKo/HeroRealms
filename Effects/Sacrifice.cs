using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sacrifice", menuName = "ScriptableObjects/Effects/Sacrifice")]
public class Sacrifice : BaseEffect
{
    public override void Activate(Player player, int amount)
    {
        for(int i=0; i<amount; i++) player.Sacrifice();
    }

}
