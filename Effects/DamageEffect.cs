using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DealDamage", menuName = "ScriptableObjects/Effects/DealDamage")]
public class DamageEffect : BaseEffect
{
    public override void Activate(Player player, int amount)
    {
        player.AddDamage(amount);
    }

}
