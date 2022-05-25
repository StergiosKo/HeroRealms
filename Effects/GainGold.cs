using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GainGold", menuName = "ScriptableObjects/Effects/GainGold")]
public class GainGold : BaseEffect
{
    public override void Activate(Player player, int amount)
    {
        player.GainGold(amount);
    }

}
