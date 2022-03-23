using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class BlackjackController : GameController
{
    private BlackjackModel blackjackModel => (BlackjackModel) gameModel;
    private BlackjackPresenter blackjackPresenter => (BlackjackPresenter) gamePresenter;

    private Player currentTurn;
    private bool[] isStanding;

    public BlackjackController(GameConfig config, BlackjackModel model, BlackjackPresenter presenter ) : base(config)
    {
        gameModel = model;
        gamePresenter = presenter;
        isStanding = new bool[2];
    }


    public override void SetupBoard()
    {
        CreateDeck();
        blackjackModel.Deck.Shuffle();
        DealCards();
        currentTurn = Player.Player;
        RunGame();
    }

    public override void CreateDeck()
    {
        for (int i = 0; i < 52; i++)
        {
            var newCard = new PlayingCard((i % 13), (i % 4));
            var added = blackjackModel.Deck.AddCardToTop(newCard);
            Debug.Log($"Added a {newCard.ToString()} to the deck. Successful = {added}.");
        }
    }

    public override void DealCards()
    {
        HitForPlayer(Player.Player);
        HitForPlayer(Player.Dealer, false);
        HitForPlayer(Player.Player);
        HitForPlayer(Player.Dealer);
    }

    public IEnumerator RunGame()
    {
        bool gameOver = false;
        while(!gameOver)
        {
            if(isStanding[(int)currentTurn])
                NextTurn();
            
            if(currentTurn == Player.Player)
            {
                Debug.Log("Awaiting Player Draw");
                yield return new WaitUntil(() => currentTurn != Player.Player);
            }
            else
            {
                var currentHandTotal = CalculateTotal(Player.Dealer, true);
                if(currentHandTotal <= 16)
                {
                    HitForPlayer(Player.Dealer);
                }
                else
                {
                    StandForPlayer(Player.Dealer);
                }
            }

            gameOver = CheckForGameOver();
        }

        Debug.Log("Game Is Over");
    }


    public override void HitForPlayer(Player player, bool faceUp = true)
    {
        var drawCard = blackjackModel.Deck.DrawCard() as PlayingCard;
        Debug.Log($"Drew the {drawCard.ToString()} for {player}");
        blackjackModel.Hands[(int) player].AddCard(drawCard, faceUp);
        CalculateTotal(player);
        UpdateScores();
        NextTurn();
    }

    public override void StandForPlayer(Player player, bool faceUp = true)
    {
        isStanding[(int)player] = true;
        NextTurn();
    }

    public override void UpdateScores()
    {
        gamePresenter.UpdateScore(Player.Player, CalculateTotal(Player.Player));
        gamePresenter.UpdateScore(Player.Dealer, CalculateTotal(Player.Dealer));
    }

    public override int CalculateTotal(Player player, bool includeFaceDown = false)
    {
        var hand = blackjackModel.Hands[(int)player];
        return hand.CalculateHandTotal(includeFaceDown);
        
    }

    public bool CheckForGameOver()
    {
        if(isStanding.All(p => p == true))
            return true;

        if(CalculateTotal(Player.Player) > 21 || CalculateTotal(Player.Dealer) > 21)
            return true;

        return false;
    }

    public void NextTurn()
    {
        currentTurn = currentTurn == Player.Player ? Player.Dealer : Player.Player;
    }
}

public class BlackjackModel : GameController.IGameModel
{
    public DeckController Deck;
    
    public DeckController DiscardPile;
    
    public HandController[] Hands;

    public BlackjackModel() 
    {
        Deck = new DeckController();
        DiscardPile = new DeckController();
        Hands = new HandController[2];
        for(int i = 0; i < Hands.Length; i++){
            Hands[i] = new HandController((GameController.Player)i);
        }
    }
}
