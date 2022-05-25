using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sacrifice", menuName = "ScriptableObjects/Costs/Sacrifice")]
public class SacrificeCost : BaseCost
{
    public override bool Check(Player player)
    {
        if (player.sacrificeThisCard) return true;
        else return false;
    }

}
