using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public Text text;

    private void UpdateHealth()
    {
        text.text = health.ToString();
    }

    public void SetHealth(int amount)
    {
        health = amount;
        CheckLimits();
        UpdateHealth();
    }

    public void IncreaseHealth(int amount)
    {
        health += amount;
        CheckLimits();
        UpdateHealth();
    }

    public void DecreaseHealth(int amount)
    {
        health -= amount;
        CheckLimits();
        UpdateHealth();
    }

    private void CheckLimits()
    {
        if (health > maxHealth) health = maxHealth;
        else if (health < 0) health = 0;
    }
}
