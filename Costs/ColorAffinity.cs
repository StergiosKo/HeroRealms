using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorAffinity", menuName = "ScriptableObjects/Costs/ColorAffinity")]
public class ColorAffinity : BaseCost
{

    public Color color;

    public override bool Check(Player player)
    {
        if (player.AffinityOnColor(color)) return true;
        else return false;
    }

}
