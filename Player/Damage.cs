using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public int damage;
    public int currentDamage;
    public Text text;

    public void UpdateDamage()
    {
        text.text = damage.ToString();
    }

    public void SetDamage(int amount)
    {
        damage = amount;
        if (damage < 0) damage = 0;
        UpdateDamage();
    }

    public void AddDamage(int amount)
    {
        damage += amount;
        UpdateDamage();
    } 

    public void Pressed()
    {
        currentDamage = damage;
        if (damage > 0) GameObject.Find("GameManager").GetComponent<GameManager>().SetDamage();
    }

    public void IncreaseCurrentDmg()
    {
        currentDamage++;
        if (currentDamage > damage) currentDamage = damage;
    }

    public void DecreaseCurrentDmg()
    {
        currentDamage--;
        if (currentDamage < 0) currentDamage = damage;
    }

    public void ShowTargets()
    {
        if (damage > 0) GameObject.Find("GameManager").GetComponent<GameManager>().ShowDamageTargets(damage);
    }

    public void DealDamage(int amount)
    {
        damage -= amount;
        UpdateDamage();
    }
}
