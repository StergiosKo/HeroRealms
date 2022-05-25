using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Card", menuName = "ScriptableObjects/NewCard/SpellCurrency")]
public class CardSO : ScriptableObject
{
    new public string name;
    public CardType cardType;
    public Color color;
    public Sprite artwork;
    public int cost;

    public List<AnEffect> effects;

}

[Serializable]
public class AnEffect
{
    public BaseCost cost;
    public BaseEffect effect;
    public int amount;

    public void Activate(Player player)
    {
        if (cost.Check(player)) effect.Activate(player, amount);
    }
}
