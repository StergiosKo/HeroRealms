using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FECGold", menuName = "ScriptableObjects/Effects/FEC/Gold")]
public class FECGold : BaseEffect
{
    public override void Activate(Player player, int amount)
    {
        player.GainGold(player.board.currentUnits * amount);
    }

}
