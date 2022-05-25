using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Card", menuName = "ScriptableObjects/NewCard/UnitCard")]
public class UnitCardSO : CardSO
{
    public int health;
    public bool guard;
}
