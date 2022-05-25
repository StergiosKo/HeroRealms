using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FEColorDamage", menuName = "ScriptableObjects/Effects/FECColor/Damage")]
public class FECColorDamage : BaseEffect
{

    public Color color;

    public override void Activate(Player player, int amount)
    {
        player.AddDamage(player.board.UnitsWithColor(color) * amount);
    }
}
