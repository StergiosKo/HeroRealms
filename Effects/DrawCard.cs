using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrawCard", menuName = "ScriptableObjects/Effects/DrawCard")]
public class DrawCard : BaseEffect
{
    public override void Activate(Player player, int amount)
    {
        for (int i = 0; i < amount; i++) player.DrawCard();
    }
}
