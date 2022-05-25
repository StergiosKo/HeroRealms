using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCost : ScriptableObject
{
    public abstract bool Check(Player player);
}
