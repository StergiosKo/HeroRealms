using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FECHeal", menuName = "ScriptableObjects/Effects/FEC/Heal")]
public class FECHeal : BaseEffect
{
    public override void Activate(Player player, int amount)
    {
        player.GainHealth(player.board.currentUnits * amount);
    }

}
