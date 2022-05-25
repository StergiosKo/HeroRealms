using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OpponentDiscard", menuName = "ScriptableObjects/Effects/OpponentDiscard")]
public class OpponentDiscard : BaseEffect
{
    public override void Activate(Player player, int amount)
    {
        player.OpponentDiscard();
    }
}
