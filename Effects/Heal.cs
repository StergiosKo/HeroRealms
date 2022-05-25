using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "ScriptableObjects/Effects/Heal")]
public class Heal : BaseEffect
{
    public override void Activate(Player player, int amount)
    {
        player.GainHealth(amount);
    }

}
