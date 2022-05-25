using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoCost", menuName = "ScriptableObjects/Costs/NoCost")]
public class NoCost : BaseCost
{
    public override bool Check(Player player)
    {
        return true;
    }
}
