using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{

    public string phase;

    public void PrintPhase()
    {
        print(phase);
    }

    public override void ReadyTurn()
    {
        hand.CanBeSeen(true);
        base.ReadyTurn();
        AITurn();
    }

    public void AITurn()
    {
        StartCoroutine(PlayTurn());
    }

    private IEnumerator PlayCards()
    {
        phase = "PlayHand";
        while (!hand.IsEmpty())
        {
            hand.holders[0].PlayThis();
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator UseUnitEffects()
    {
        phase = "Use Unit Effects";
        for(int i=0; i<board.currentUnits; i++)
        {
            board.holders[i].DoEffect();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator PlayTurn()
    {
        while (!hand.IsEmpty())
        {
            yield return StartCoroutine(PlayCards());
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(UseUnitEffects());
        }
        yield return StartCoroutine(UseGold());
        yield return StartCoroutine(UseDamage());
        hand.CanBeSeen(false);
        GameObject.Find("GameManager").GetComponent<GameManager>().EndTurn();
    }

    // Currency

    private IEnumerator BuyCard()
    {
        phase = "Buy Cards";
        BuyDeckHandler handler = GameObject.Find("BuyDeckHandler").GetComponent<BuyDeckHandler>();
        int gold = currencyManager.GetCurrency();
        CardSlots cardToBuy = handler.HighestCostCard(gold);
        if (cardToBuy != null)
        {
            handler.BuyCard(cardToBuy);
        }
        else
        {
            if (gold == 2) handler.BuyCurrency();
            else currencyManager.SetCurrency(0);
        }
        yield return new WaitForSeconds(1.2f);
        
    }

    private IEnumerator UseGold()
    {
        while(currencyManager.GetCurrency() > 0)
        {
            yield return StartCoroutine(BuyCard());
        }
    }

    // Deal Damage

    private IEnumerator UseDamage()
    {
        phase = "Use Damage";
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        List<Player> enemyPlayers = new List<Player>();
        while (damage.damage > 0)
        {
            enemyPlayers.Clear();
            foreach (Player player in manager.players)
            {
                if (player != this) enemyPlayers.Add(player);
            }
            Player strongestOpponent = FindStrongestOpponent(enemyPlayers);
            if (strongestOpponent != null)
            {
                UnitHolder target = FindTarget(strongestOpponent);
                if (target != null)
                {
                    UnitCardSO unit = (UnitCardSO)target.baseCard.cardSO;
                    strongestOpponent.UnitDied(target.baseCard);
                    damage.DealDamage(unit.health);
                }
                else
                {
                    strongestOpponent.LoseHealth(damage.damage);
                    damage.SetDamage(0);
                }
            }
            else
            {
                damage.SetDamage(0);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private Player FindStrongestOpponent(List<Player> players)
    {
        List<Threat> playerThreats = new List<Threat>();
        foreach(Player player in players)
        {
            playerThreats.Add(new Threat(player));
        }
        List<Threat> damagableThreats = new List<Threat>();
        foreach(Threat threat in playerThreats)
        {
            if (IsEnemyDamagable(threat.player)) damagableThreats.Add(threat);
        }

        if (damagableThreats.Count == 0) return null;
        else
        {
            Threat strongestOpponent = damagableThreats[0];
            foreach(Threat threat in damagableThreats)
            {
                if (strongestOpponent.threat < threat.threat) strongestOpponent = threat;
            }
            return strongestOpponent.player;
        }

    }

    private bool IsEnemyDamagable(Player player)
    {
        if (player.board.FindGuard())
        {
            List<UnitHolder> guards = player.board.ReturnGuards();
            foreach(UnitHolder holder in guards)
            {
                UnitCardSO unit = (UnitCardSO) holder.baseCard.cardSO;
                if (unit.health <= damage.damage) return true;
            }
        }
        return true;
    }

    private UnitHolder FindStrongestUnit(List<UnitHolder> units)
    {
        if (units.Count > 0)
        {
            UnitHolder strongestUnit = units[0];
            foreach (UnitHolder unit in units)
            {
                if (unit.GetCost() > strongestUnit.GetCost()) strongestUnit = unit;
            }
            return strongestUnit;
        }
        else
        {
            return null;
        }

    }

    private UnitHolder FindTarget(Player player)
    {
        if (player.board.FindGuard())
        {
            List<UnitHolder> guards = player.board.ReturnGuards();
            return FindStrongestUnit(FindDamagableUnits(guards));
        }
        else
        {
            return FindStrongestUnit(FindDamagableUnits(player.board.GetUnits()));
        }
    }

    private List<UnitHolder> FindDamagableUnits(List<UnitHolder> playerUnits)
    {
        List<UnitHolder> units = new List<UnitHolder>();
        foreach (UnitHolder unit in playerUnits)
        {
            UnitCardSO card = (UnitCardSO)unit.baseCard.cardSO;
            if (card.health <= damage.damage) units.Add(unit);
        }
        return units;
    }

    // Card Effects

    public override void DiscardCard()
    {
        if (!hand.IsEmpty())
        {
            List<CardHolder> cardsInHand = hand.GetCards();
            Card lowerCostCard = cardsInHand[0].baseCard;
            foreach(CardHolder holder in cardsInHand)
            {
                if (lowerCostCard.cardSO.cost > holder.baseCard.cardSO.cost) lowerCostCard = holder.baseCard;
            }
            hand.RemoveCard(lowerCostCard);
            AddCardGY(lowerCostCard);
        }
    }

    public override void StunUnit()
    {
        List<Player> enemyPlayers = GameObject.Find("GameManager").GetComponent<GameManager>().FindOpponents(this);
        List<Player> playersWithUnits = new List<Player>();
        foreach(Player player in enemyPlayers)
        {
            if (!player.board.IsEmpty()) playersWithUnits.Add(player);
        }
        if (playersWithUnits.Count > 0)
        {
            Player strongestOpponnent = FindStrongestOpponent(playersWithUnits);
            UnitHolder unit;
            if (strongestOpponnent.board.FindGuard())
            {
                unit = FindStrongestUnit(strongestOpponnent.board.ReturnGuards());
            }
            else
            {
                unit = FindStrongestUnit(strongestOpponnent.board.GetUnits());
            }
            strongestOpponnent.UnitDied(unit.baseCard);
        }
    }

    public override void Prepare()
    {
        if (!board.IsEmpty())
        {
            List<UnitHolder> units = board.GetUnits();
            UnitHolder strongestUnit = units[0];
            foreach(UnitHolder holder in units)
            {
                if (holder.baseCard.cardSO.cost > strongestUnit.baseCard.cardSO.cost) strongestUnit = holder;
            }
            strongestUnit.Ready();
        }
    }

    public override void OpponentDiscard()
    {
        List<Player> enemyPlayers = GameObject.Find("GameManager").GetComponent<GameManager>().FindOpponents(this);
        Player opponent =  FindStrongestOpponent(enemyPlayers);
        opponent.DiscardCard();
    }

    public override void Sacrifice()
    {
        if (graveyard.GetSize() > 0)
        {
            Card cardToSacrifice = graveyard.cards[0];
            foreach(Card card in graveyard.cards)
            {
                if (card.cardSO.cost < cardToSacrifice.cardSO.cost) cardToSacrifice = card;
            }
            SacrificeCardGY(cardToSacrifice);
            CloseGraveyard();
        }
        else if (!hand.IsEmpty())
        {
            Card cardToSacrifice = hand.cards[0];
            foreach (Card card in hand.cards)
            {
                if (card.cardSO.cost < cardToSacrifice.cardSO.cost) cardToSacrifice = card;
            }
            SacrificeCardHand(cardToSacrifice);
        }
    }

    public override void SacrificableCard(Card card)
    {
        sacrificeThisCard = false;
        PlayCard(card);
    }

    public override void ReturnCardFromGY(List<CardType> cardTypes)
    {
        if (graveyard.GetSize() > 0)
        {
            List<Card> availableCards = new List<Card>();
            foreach (Card card in graveyard.cards)
            {
                if (cardTypes.Contains(card.cardSO.cardType)) availableCards.Add(card);
            }
            if (availableCards.Count > 0) 
            {
                Card cardToReturn = availableCards[0];
                foreach (Card card in availableCards)
                {
                    if (card.cardSO.cost < cardToReturn.cardSO.cost) cardToReturn = card;
                }
                CardReturnedFromGY(cardToReturn);
            }

        }
    }
}

public class Threat
{
    public Player player;
    public int threat;

    public Threat(Player player)
    {
        this.player = player;
        CalculateThreat();
    }

    public void CalculateThreat()
    {
        foreach(UnitCard unit in player.board.units)
        {
            threat += unit.cardSO.cost;
        }
        threat += player.health.health / 2;
    }
}
