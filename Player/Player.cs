using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Hand hand;
    public Deck deck;
    public Deck graveyard;
    public Health health;
    public Damage damage;
    public Icon icon;

    public CurrencyManager currencyManager;

    public Board board;
    public GraveyardCards graveyardCards;

    public GameObject unitDeathAnimation;

    public PlayerSO playerSO;

    private bool myTurn;
    private List<Color> colorAfinity = new List<Color>();
    private bool graveyardOpen = false;

    // Choices Variables

    public bool sacrificeThisCard = false;

    public bool choiceChosen = false;

    // Buy Cards Variables

    readonly List<CardType> typeToTop = new List<CardType>();
    public bool returnToHand = false;


    // Effect Variables
    private bool unitPlayed;
    private int cardsToDiscard = 0;

    public void PrepareStart(int healthStart)
    {
        if (playerSO != null)
        {
            icon.ChangeIcon(playerSO.playerSprite);
            icon.ChangeName(playerSO.playerName);
        }
        GetBaseDeck();
        graveyard.SetCount();
        health.SetHealth(healthStart);
        hand.CanBeSeen(false);
    }

    public void EndTurn()
    {
        typeToTop.Clear();
        colorAfinity.Clear();
        icon.CloseBackground();
        myTurn = false;
        board.UnreadyBoard();
        RemoveHand();
        currencyManager.SetCurrency(0);
        damage.SetDamage(0);
        NewHand();
        graveyard.SetCount();
        returnToHand = false;
        DiscardReset();
    }

    public virtual void ReadyTurn()
    {
        hand.CanBeSeen(true);
        DiscardReset();
        GetColorAffinity();
        icon.ShowBackground(Background.Active);
        board.ReadyBoard();
        myTurn = true;
        unitPlayed = false;
    }

    public void NewHand()
    {
        for(int i=0; i<5; i++)
        {
            DrawCard();
        }
        hand.UpdateHand();
    }

    public void DrawCards(int amount)
    {
        for(int i=0; i<amount; i++)
        {
            DrawCard();
        }
    }

    public void RemoveHand()
    {
        hand.RemoveHand(this);
    }

    public void DrawCard()
    {

        if ((deck.GetSize() == 0))
        {
            deck.CopyDeck(graveyard);
            deck.Shuffle();
            graveyard.Empty();
        }

        if (deck.GetSize() > 0)
        {
            if (!hand.Limit())
            {
                Card card = deck.Draw();
                colorAfinity.Add(card.cardSO.color);
                hand.AddCard(card);
            } 
            else AddCardGY(deck.Draw());

            hand.UpdateHand();
        }

    }

    public void GetBaseDeck()
    {
        CardManager cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        cardManager.GiveDeck(this);
        deck.Shuffle();
    }

    public void AddCardDeck(Card card)
    {
        deck.AddCard(card);
    }

    public bool TryBuyCard(Card card)
    {
        if (currencyManager.CheckEnough(card.GetCost()))
        {
            if (typeToTop.Contains(card.cardSO.cardType))
            {
                deck.AddInTop(card);
                typeToTop.Remove(card.cardSO.cardType);
                if (returnToHand)
                {
                    DrawCard();
                    returnToHand = false;
                }
            }
            else
            {
                AddCardGY(card);
            }
            LoseGold(card.GetCost());
            return true;
        }
        else return false;
        
    }

    public void UnitDied(Card card)
    {
        board.RemoveUnit(card);
        AddCardGY(card);
    }


    public void PlayUnit(Card card)
    {
        unitPlayed = true;
        if (!board.Limit()) board.AddUnit(card);
        else AddCardGY(card);
    }

    public void PlayCard(Card card)
    { 
        card.Play(this);
        hand.RemoveCard(card);
    }

    public void GainHealth(int amount)
    {
        health.IncreaseHealth(amount);
    }

    public void LoseHealth(int amount)
    {
        icon.DamagedAnimation();
        health.DecreaseHealth(amount);
        if (health.health <= 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().PlayerDied(this);
        }
    }

    public void AddDamage(int amount)
    {
        damage.AddDamage(amount);
    }

    public void DealDamage(Player player, int amount)
    {
        if (player.health.health > amount)
        {
            player.LoseHealth(amount);
            damage.DealDamage(amount);
        }
        else
        {
            damage.DealDamage(player.health.health);
            player.LoseHealth(amount);
        }

    }

    public void GainGold(int amount)
    {
        currencyManager.AddCurrency(amount);
    }

    public void LoseGold(int amount)
    {
        currencyManager.RemoveCurrency(amount);
    }

    public void ReadyUnit(UnitCard card)
    {
        board.ReadyUnit(card);
    }

    public bool Turn()
    {
        return myTurn;
    }

    public void ShowDamagable(int amount)
    {
        if (board.FindGuard()) board.ShowDamagable(true, amount);
        else
        {
            board.ShowDamagable(false, amount);
            icon.ShowBackground(Background.Choose);
        }
    }

    public void ShowStunnable()
    {
        if (board.FindGuard()) board.ShowDamagable(true, 99);
        else
        {
            board.ShowDamagable(false, 99);
        }
    }

    public virtual void StunUnit()
    {
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (!manager.CheckOpponentEmptyBoard(this)) manager.ShowStunTargets();
    }

    public void RemoveDamage(int amount)
    {
        damage.DealDamage(amount);
    }

    public void RemoveTargets()
    {
        board.RemoveTargets();
        icon.CloseBackground();
    }

    public virtual void DiscardCard()
    {
        if (!hand.IsEmpty())
        {
            cardsToDiscard++;
            hand.CardsDiscardable(true);
            GameObject.Find("GameManager").GetComponent<GameManager>().PlayerDiscard();
        }

    }

    private void DiscardReset()
    {
        cardsToDiscard = 0;
        if (cardsToDiscard == 0 || hand.IsEmpty())
        {
            hand.CardsDiscardable(false);
            GameObject.Find("GameManager").GetComponent<GameManager>().CardDiscarded();
        }
    }

    public void ThisCardDiscard(Card card)
    {
        cardsToDiscard--;
        hand.RemoveCard(card);
        AddCardGY(card);
        if (cardsToDiscard == 0 || hand.IsEmpty())
        {
            hand.CardsDiscardable(false);
            GameObject.Find("GameManager").GetComponent<GameManager>().CardDiscarded();
        }
    }

    private void GetColorAffinity()
    {
        colorAfinity.AddRange(board.GetColorAffinity());
    }

    public bool AffinityOnColor(Color color)
    {
        int colorTimes = 0;
        for(int i=0; i<colorAfinity.Count; i++)
        {
            if (color == colorAfinity[i]) colorTimes++;
        }
        if (colorTimes > 1) return true;
        else return false;
    }

    public void UnitPreparation(bool ready)
    {
        board.UnitPreparation(ready);
    }

    public virtual void Prepare()
    {
        if (!board.IsEmpty()) GameObject.Find("GameManager").GetComponent<GameManager>().PrepareUnits(this);
    }

    private void SeeGraveyard()
    {
        graveyardOpen = true;
        graveyardCards.gameObject.SetActive(true);
        foreach(Card card in graveyard.cards)
        {
            graveyardCards.CreateCard(card);
        }
    }

    protected void CloseGraveyard()
    {
        graveyardOpen = false;
        graveyardCards.DestroyCards();
        graveyardCards.gameObject.SetActive(false);
    }

    public void GraveyardClicked()
    {
        if (!graveyardOpen) SeeGraveyard();
        else CloseGraveyard();
    }

    public bool IsCardInGraveyard(Card card)
    {
        foreach (Card gyCard in graveyard.cards)
        {
            if (gyCard == card) return true;
        }
        return false;
    }

    public void SacrificeCardGY(Card card)
    {
        graveyard.RemoveCard(card);
        graveyardCards.DestroyCards();
        SeeGraveyard();
        GameObject.Find("GameManager").GetComponent<GameManager>().CardSacrificed();
    }


    public void SacrificeCardHand(Card card)
    {
        hand.RemoveCard(card);
        GameObject.Find("GameManager").GetComponent<GameManager>().CardSacrificed();
    }

    public virtual void Sacrifice()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().Sacrifice();
    }

    public void UpdateGraveyard()
    {
        graveyardCards.DestroyCards();
        SeeGraveyard();
    }

    public void AddCardGY(Card card)
    {
        graveyard.AddCard(card);
        if (graveyardCards.gameObject.activeSelf) UpdateGraveyard();
    }

    public void PlaySacrificeCard(Card card)
    {
        sacrificeThisCard = true;
        StartCoroutine(PlaySacrificedCard(card));
    }

    private IEnumerator SacrificePlayedCard(Card card)
    {
        if (!graveyardOpen) graveyard.RemoveCard(card);
        else SacrificeCardGY(card);
        yield return null;
        sacrificeThisCard = false;
    }

    private IEnumerator PlaySacrificedCard(Card card)
    {   
        PlayCard(card);
        yield return null;
        StartCoroutine(SacrificePlayedCard(card));
    }

    public virtual void SacrificableCard(Card card)
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().SacrificableCardPlayed(card);
    }

    // For buy Effect
    public void AddCardTypeToTop(CardType type)
    {
        if (!typeToTop.Contains(type)) typeToTop.Add(type);
    }

    public void RemoveCardTypeToTop(CardType type)
    {
        if (typeToTop.Contains(type)) typeToTop.Remove(type);
    }

    // For Return Effect

    public virtual void ReturnCardFromGY(List<CardType> cardTypes)
    {
        if (graveyard.GetSize() > 0) GameObject.Find("GameManager").GetComponent<GameManager>().ReturnCardGY(cardTypes);
    }

    public void CardReturnedFromGY(Card card)
    {
        graveyard.RemoveCard(card);
        if (graveyardOpen)
        {
            graveyardCards.DestroyCards();
            SeeGraveyard();
        }
        deck.AddInTop(card);
        GameObject.Find("GameManager").GetComponent<GameManager>().CardReturned();
    }

    public virtual void OpponentDiscard()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().OpponentDiscard();
    }

    // Check if unit played this turn

    public bool UnitPlayedThisTurn()
    {
        return unitPlayed;
    }

    // debug

    public void PrintAffinity()
    {
        foreach(Color color in colorAfinity)
        {
            if (AffinityOnColor(color)) Debug.Log(color);
        }
    }

    public bool DoneWithDiscard()
    {
        if (cardsToDiscard == 0) return true;
        else return false;
    }

}
