using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public List<UnitHolder> holders;
    public List<Card> units = new List<Card>();

    public int maxUnits;
    public int currentUnits;

    private void Start()
    {
        currentUnits = 0;
        DeactivateHolders();
    }

    public bool Limit()
    {
        if (currentUnits == maxUnits) return true;
        else return false;
    }

    public void DeactivateHolders()
    {
        foreach(UnitHolder holder in holders)
        {
            holder.gameObject.SetActive(false);
        }
    }

    public UnitHolder FindUnit(Card card)
    {
        for (int i = 0; i < currentUnits; i++)
        {
            if (card == holders[i].baseCard) return holders[i];
        }
        return null;
    }

    public void UpdateBoard()
    {
        DeactivateHolders();
        for(int i=0; i<currentUnits; i++)
        {
            holders[i].gameObject.SetActive(true);
            holders[i].SetCard(units[i]);
        }
    }

    public void RemoveUnit(Card card)
    {
        StartCoroutine(UnitDied(card));
    }

    private IEnumerator UnitDied(Card card)
    {
        yield return DeathAnimation(FindUnit(card));
    }

    private IEnumerator DeathAnimation(UnitHolder holder)
    {
        holder.deathAnimation.SetActive(true);
        yield return new WaitForSeconds(1f);
        holder.deathAnimation.SetActive(false);
        yield return StartCoroutine(RemoveUnitCoroutine(holder.baseCard));
    }

    private IEnumerator RemoveUnitCoroutine(Card card)
    {
        units.Remove(card);
        currentUnits--;
        UpdateBoard();
        yield return null;
    }

    public void AddUnit(Card card)
    {
        units.Add(card);
        currentUnits++;
        UpdateBoard();
        FindUnit(card).Ready();
    }

    public void ReadyBoard()
    {
        for (int i = 0; i < currentUnits; i++)
        {
            holders[i].Ready();
        }
    }

    public void ReadyUnit(Card card)
    {
        foreach (UnitHolder holder in holders)
        {
            if (holder.baseCard == card)
            {
                holder.Ready();
            }
        }
    }

    public void UnreadyBoard()
    {
        for (int i = 0; i < currentUnits; i++)
        {
            holders[i].Unready();
        }
    }

    public void ShowDamagable(bool guarded, int amount)
    {
        if (guarded)
        {
            for (int i = 0; i < currentUnits; i++)
            {
                if (holders[i].IsGuard()) holders[i].Damagable(amount);
            }
        }
        else
        {
            for (int i = 0; i < currentUnits; i++)
            {
                holders[i].Damagable(amount);
            }
        }
    }

    public bool FindGuard()
    {
        for (int i = 0; i < currentUnits; i++)
        {
            if (holders[i].IsGuard()) return true;
        }

        return false;
    }

    public void RemoveTargets()
    {
        foreach (UnitHolder holder in holders)
        {
            holder.CloseBackground();
        }
    }

    public List<Color> GetColorAffinity()
    {
        List<Color> colorList = new List<Color>();
        for (int i = 0; i < currentUnits; i++)
        {
            colorList.Add(holders[i].cardSO.color);
        }
        return colorList;
    }

    public void UnitPreparation(bool flag)
    {
        for (int i = 0; i < currentUnits; i++)
        {
            holders[i].Prepared(flag);
        }
    }

    public bool IsEmpty()
    {
        if (currentUnits > 0) return false;
        else return true;
    }

    public void RemoveBoard()
    {
        currentUnits = 0;
        units.Clear();
        UpdateBoard();
    }

    public List<UnitHolder> ReturnGuards()
    {
        List<UnitHolder> guards = new List<UnitHolder>();
        for(int i=0; i<currentUnits; i++)
        {
            if (holders[i].IsGuard()) guards.Add(holders[i]);
        }
        return guards;
    }

    public List<UnitHolder> GetUnits()
    {
        List<UnitHolder> availableUnits = new List<UnitHolder>();
        for(int i=0; i < currentUnits; i++)
        {
            availableUnits.Add(holders[i]);
        }
        return availableUnits;
    }

    public int UnitsWithColor(Color color)
    {
        int num = 0;
        for(int i=0; i<currentUnits; i++)
        {
            if (holders[i].baseCard.cardSO.color == color) num++;
        }
        return num;
    }


}
