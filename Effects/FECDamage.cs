using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FECDamage", menuName = "ScriptableObjects/Effects/FEC/Damage")]
public class FECDamage : BaseEffect
{
    public override void Activate(Player player, int amount)
    {
        player.AddDamage(player.board.currentUnits * amount);
    }
}
