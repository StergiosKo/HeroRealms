using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Choice
{
    Yes,
    No
}

public class GameManager : MonoBehaviour
{
    public List<Player> players;
    public List<PlayerSO> playersSO;

    public List<Player> humanPlayers;
    public List<AIPlayer> aiPlayers;
    public static Player currentPlayer;

    public BuyDeckHandler buyDeckHandler;

    public Text reminderText;

    private int currentDmg;

    public static bool someoneChoosing = false;
    public static bool chooseDmg = false;
    public static bool chooseDiscard = false;
    public static bool chooseStun = false;
    public static bool chooseSacrifice = false;
    public static bool returnCardDeck = false;
    public static List<CardType> cardTypes = new List<CardType>();
    public static bool opponentDiscards = false;

    private bool waitChoice = false;
    private bool currentChoice = false;
    public bool done = false;

    public int cardsToDiscard = 0;

    public GameObject endTurnButton;
    public GameObject yesButton;
    public GameObject noButton;

    public GameObject buttonUp;
    public GameObject buttonDown;
    public GameObject buttonOK;

    public GameObject swordIcon;


    private void Start()
    {
        for(int i=0; i<playersSO.Count; i++)
        {
            if (playersSO[i].isActive)
            {
                if (playersSO[i].playerType == PlayerType.Human)
                {
                    players.Add(humanPlayers[i]);
                    humanPlayers[i].gameObject.SetActive(true);
                    humanPlayers[i].playerSO = playersSO[i];
                }
                else
                {
                    players.Add(aiPlayers[i]);
                    aiPlayers[i].gameObject.SetActive(true);
                    aiPlayers[i].playerSO = playersSO[i];
                }
            }
        }

        foreach(Player player in players)
        {
            player.PrepareStart(60);
        }

        if (players.Count == 4)
        {
            players[0].DrawCards(3);
            players[1].DrawCards(4);
            players[2].NewHand();
            players[3].NewHand();
        }
        else if (players.Count == 3)
        {
            players[0].DrawCards(3);
            players[1].DrawCards(4);
            players[2].NewHand();
        }
        else
        {
            players[0].NewHand();
            players[1].NewHand();
        }
        currentPlayer = players[0];
        ReadyCurrentPlayer();
    }

    public void PlayerDied(Player player)
    {
        player.icon.CloseBackground();
        player.RemoveHand();
        player.board.RemoveBoard();
        currentPlayer.GainHealth(20);
        currentPlayer.DrawCard();
        currentPlayer.DrawCard();
        players.Remove(player);
        if (players.Count == 1)
        {
            gameObject.GetComponent<MoveSceen>().Pressed("StartingScene");
        }
    }

    public List<Player> FindOpponents(Player thisPlayer)
    {
        List<Player> opponents = new List<Player>();
        foreach(Player player in players)
        {
            if (player != thisPlayer) opponents.Add(player);
        }
        return opponents;
    }

    public void EndTurn()
    {
        currentPlayer.EndTurn();
        int index = players.IndexOf(currentPlayer) + 1;
        if (index > players.Count - 1) index = 0;
        currentPlayer = players[index];
        ReadyCurrentPlayer();
    }

    private void ReadyCurrentPlayer()
    {
        if (currentPlayer.GetType() == typeof(Player)) endTurnButton.SetActive(true);
        else endTurnButton.SetActive(false);
        currentPlayer.ReadyTurn();
        buyDeckHandler.currentPlayer = currentPlayer;
    }

    public void ShowDamageTargets(int amount)
    {
        buttonDown.gameObject.SetActive(false);
        buttonUp.gameObject.SetActive(false);
        buttonOK.gameObject.SetActive(false);
        reminderText.transform.parent.gameObject.SetActive(true);
        reminderText.text = "Choose where to deal [" + amount.ToString() + "] damage";
        someoneChoosing = true;
        chooseDmg = true;
        currentDmg = amount;
        foreach(Player player in players)
        {
            if (player != currentPlayer)
            {
                player.ShowDamagable(amount);
            }
        }
        StartCoroutine(ChooseDmgTarget());
    }

    public void ShowStunTargets()
    {
        reminderText.transform.parent.gameObject.SetActive(true);
        reminderText.text = "Choose which Unit to Stun";
        someoneChoosing = true;
        chooseStun = true;
        foreach (Player player in players)
        {
            if (player != currentPlayer)
            {
                player.ShowStunnable();
            }
        }
        StartCoroutine(ChooseStun());
    }

    public bool CheckOpponentEmptyBoard(Player crPlayer)
    {
        int units = 0;
        foreach (Player player in players)
        {
            if (player != crPlayer)
            {
                if (player.board.currentUnits > 0) units++;
            }
        }
        if (units > 0) return false;
        else return true;
    }

    private void RemoveDamageTargets()
    {
        foreach (Player player in players)
        {
            if (player != currentPlayer)
            {
                player.RemoveTargets();
            }
        }
    }

    public void UndoPressed()
    {
        cardsToDiscard = 0;
        opponentDiscards = false;
        cardTypes.Clear();
        reminderText.transform.parent.gameObject.SetActive(false);
        done = true;
        currentDmg = 0;
        someoneChoosing = false;
        chooseDmg = false;
        chooseDiscard = false;
        chooseStun = false;
        chooseSacrifice = false;
        RemoveDamageTargets();
        returnCardDeck = false;
        buttonDown.gameObject.SetActive(false);
        buttonUp.gameObject.SetActive(false);
        buttonOK.gameObject.SetActive(false);
    }

    public void SetDamage()
    {
        reminderText.transform.parent.gameObject.SetActive(true);
        buttonDown.gameObject.SetActive(true);
        buttonUp.gameObject.SetActive(true);
        buttonOK.gameObject.SetActive(true);
        reminderText.text = "Deal [" + currentPlayer.damage.currentDamage.ToString() + "] damage?";
    }

    public void IncreaseDamage()
    {
        currentPlayer.damage.IncreaseCurrentDmg();
        reminderText.text = "Deal [" + currentPlayer.damage.currentDamage.ToString() + "] damage?";
    }

    public void DecreaseDamage()
    {
        currentPlayer.damage.DecreaseCurrentDmg();
        reminderText.text = "Deal [" + currentPlayer.damage.currentDamage.ToString() + "] damage?";
    }

    public void OkDamage()
    {
        ShowDamageTargets(currentPlayer.damage.currentDamage);
    }

    public void DamagePlayerClicked(Player player)
    {
        currentPlayer.DealDamage(player, currentDmg);
        currentDmg = 0;
        done = true;
        someoneChoosing = false;
        chooseDmg = false;
        RemoveDamageTargets();
    }

    public void DamageUnitClicked(int unitHealth)
    {
        currentPlayer.RemoveDamage(unitHealth);
        done = true;
        someoneChoosing = false;
        chooseDmg = false;
        RemoveDamageTargets();
    }


    public void Sacrifice()
    {
        reminderText.transform.parent.gameObject.SetActive(true);
        reminderText.text = "Choose which card to Sacrifice";
        done = false;
        someoneChoosing = true;
        chooseSacrifice = true;
    }

    public void CardSacrificed()
    {
        reminderText.transform.parent.gameObject.SetActive(false);
        done = true;
        someoneChoosing = false;
        chooseSacrifice = false;
    }

    public void CardDiscarded()
    {
        someoneChoosing = false;
        done = true;
        chooseDiscard = false;
    }

    public void ReturnCardGY(List<CardType> theCardTypes)
    {
        foreach(CardType type in theCardTypes)
        {
            cardTypes.Add(type);
        }
        reminderText.transform.parent.gameObject.SetActive(true);
        if (cardTypes.Count > 1)
        {
            reminderText.text = "Select a card from GY to return to Deck";
        }
        else if (cardTypes.Count == 1)
        {
            reminderText.text = "Select a " + cardTypes[0].ToString() + " card from GY to return to Deck";
        }
        else
        {
            reminderText.text = "Select a card? from GY to return to Deck";
        }
        returnCardDeck = true;
        someoneChoosing = true;
        done = false;
    }

    public void CardReturned()
    {
        reminderText.transform.parent.gameObject.SetActive(false);
        returnCardDeck = false;
        someoneChoosing = false;
        done = true;
    }


    public void UnitStunned()
    {
        someoneChoosing = false;
        done = true;
        chooseStun = false;
        RemoveDamageTargets();
    }

    public void CurrentPlayerDiscard()
    {
        currentPlayer.DiscardCard();
    }

    public void PrepareUnits(Player player)
    {
        reminderText.transform.parent.gameObject.SetActive(true);
        reminderText.text = "Select a Unit to Prepare";
        someoneChoosing = true;
        done = false;
        player.UnitPreparation(true);
    }

    public void UnitPrepared(Player player)
    {
        reminderText.transform.parent.gameObject.SetActive(false);
        someoneChoosing = false;
        done = true;
        player.UnitPreparation(false);
    }

    public void PlayerDiscard()
    {
        reminderText.transform.parent.gameObject.SetActive(true);
        reminderText.text = "Discard a Card";
        someoneChoosing = true;
        done = false;
        chooseDiscard = true;
        StartCoroutine(ChooseDiscard());
    }

    private IEnumerator ChooseDmgTarget()
    {
        yield return WaitForTarget();
    }

    private IEnumerator ChooseDiscard()
    {
        yield return WaitForTarget();
    }

    private IEnumerator ChooseStun()
    {
        yield return WaitForTarget();
    }

    private IEnumerator WaitForTarget()
    {
        done = false;
        while (!done)
        {
            yield return null;
        }
        reminderText.transform.parent.gameObject.SetActive(false);
    }

    public void SelectChoice()
    {
        currentChoice = false;
        done = false;
        waitChoice = true;
        reminderText.transform.parent.gameObject.SetActive(true);
        reminderText.text = "Sacrifice Card?";
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
    }

    public void ChoiceSelected()
    {
        cardTypes.Clear();
        done = true;
        waitChoice = false;
        reminderText.transform.parent.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
    }

    public void YesPressed()
    {
        ChoiceSelected();
        currentChoice = true;
    }

    public void NoPressed()
    {
        ChoiceSelected();
        currentChoice = false;
    }

    private IEnumerator WaitChoice(Card card)
    {
        while (waitChoice)
        {
            yield return null;
        }
        if (currentChoice)
        {
            currentPlayer.PlaySacrificeCard(card);
        }
        else
        {
            currentPlayer.PlayCard(card);
        }
    }

    public void SacrificableCardPlayed(Card card)
    {
        SelectChoice();
        StartCoroutine(WaitChoice(card));
    }

    public void OpponentDiscard()
    {
        PlayerDiscard();
        opponentDiscards = true;
        foreach(Player player in players)
        {
            if (player != currentPlayer)
            {
                player.icon.ShowBackground(Background.Choose);
            }
        }
    }

    public void ShowAffinity()
    {
        currentPlayer.PrintAffinity();
    }

}
