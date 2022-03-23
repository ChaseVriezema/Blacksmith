using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlackjackPresenter : MonoBehaviour, GameController.IGamePresenter
{
    [SerializeField] private Button hitButton;
    [SerializeField] private Button standButton;

    [SerializeField] private HandPresenter playerHand;
    [SerializeField] private HandPresenter dealerHand;

    [SerializeField] private DeckPresenter deckPresenter;
    
    [SerializeField] private TextMeshProUGUI playerScore;
    [SerializeField] private TextMeshProUGUI dealerScore;

    [SerializeField] private TextMeshProUGUI messageText;


    [SerializeField] private GameObject cardPrefab;

    public void InitDeck()
    {

    }

    public void UpdateScore(GameController.Player player, int total)
    {
        switch(player)
        {
            case GameController.Player.Player:
                playerScore.text = total.ToString();
                break;
            case GameController.Player.Dealer:
                dealerScore.text = total.ToString();
                break;
        }

    }

    public void DrawCardForPlayer(CardBase card, GameController.Player player, bool faceUp = true)
    {
        var cardObj = CreateCardOnDeck(card);
        var slot = player == GameController.Player.Player ? playerHand : dealerHand;

        //lerp card to slot
    }

    public void StandForPlayer(GameController.Player player)
    {

    }

    public void ChangeCurrentTurn(GameController.Player player)
    {

    }

    public void ClearBoard()
    {

    }

    public void ShowMessage(string text, float duration)
    {

    }

    public GameObject CreateCardOnDeck(CardBase card)
    {
        return null;
    }
}