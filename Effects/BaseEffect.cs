using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Effect
{
    Draw,
    Dicard,
    Damage,
    Heal,
    Gold,
    Stun,
    Prepare,
    Sacrifice
}

public abstract class BaseEffect : ScriptableObject
{
    public abstract void Activate(Player player, int amount);

}
