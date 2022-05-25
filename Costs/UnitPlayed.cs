using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitPlayed", menuName = "ScriptableObjects/Costs/UnitPlayed")]
public class UnitPlayed : BaseCost
{
    public override bool Check(Player player)
    {
        return player.UnitPlayedThisTurn();
    }

}
