using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHolder : CardHolder
{

    public Player player;

    public Image background;

    public GameObject deathAnimation;

    private bool damagable = false;
    private bool prepare = false;

    public void DoEffect()
    {
        if (!GameManager.someoneChoosing)
        {
            baseCard.UseEffects(player);
            CloseBackground();
        }

    }

    public void RemoveUnit()
    {
        player.UnitDied(this.baseCard);
    }


    public void OpenBackground()
    {
        background.gameObject.SetActive(true);
        background.color = new Color32(241, 26, 228, 255);
    }

    public void Damagable(int amount)
    {
        if (IsDamagable(amount))
        {
            damagable = true;
            background.gameObject.SetActive(true);
            background.color = new Color32(29, 171, 242, 255);
        }
    }

    public void CloseBackground()
    {
        damagable = false;
        background.gameObject.SetActive(false);
    }

    public void Ready()
    {
        UnitCard unit = (UnitCard)baseCard;
        unit.Ready();
        OpenBackground();
    }

    public void Unready()
    {
        UnitCard unit = (UnitCard)baseCard;
        unit.Unready();
        CloseBackground();
    }

    public bool IsDamagable(int amount)
    {
        UnitCardSO card = (UnitCardSO)cardSO;
        if (card.health <= amount) return true;
        else return false;
    }

    public bool IsGuard()
    {
        if (baseCard != null)
        {
            UnitCardSO card = (UnitCardSO)baseCard.cardSO;
            if (card.guard) return true;
            else return false;
        }
        else return false;
    }

    public void EnemyClicked()
    {
        if (damagable)
        {
            damagable = false;
            RemoveUnit();
            if (GameManager.chooseStun)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().UnitStunned();
            }
            else
            {
                UnitCardSO unit = (UnitCardSO)cardSO;
                GameObject.Find("GameManager").GetComponent<GameManager>().DamageUnitClicked(unit.health);
            }
        }
        else if (prepare)
        {
            prepare = false;
            Ready();
            GameObject.Find("GameManager").GetComponent<GameManager>().UnitPrepared(player);
        }

    }

    public void Prepared(bool flag)
    {
        prepare = flag;
    }

    public int GetCost()
    {
        return baseCard.cardSO.cost;
    }
}
