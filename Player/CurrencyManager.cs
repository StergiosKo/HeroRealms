using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    private int currency = 0;
    public Text text;

    public void AddCurrency(int amount)
    {
        currency += amount;
        UpdateText();
    }

    public void SetCurrency(int amount)
    {
        currency = amount;
        UpdateText();
    }

    public void RemoveCurrency(int amount)
    {
        currency -= amount;
        UpdateText();
    }

    public bool CheckEnough(int amount)
    {
        if (amount > currency) return false;
        else return true;
    }

    public void ClearCurrency()
    {
        currency = 0;
        UpdateText();
    }

    public void UpdateText()
    {
        text.text = currency.ToString();
    }

    public int GetCurrency()
    {
        return currency;
    }

}
