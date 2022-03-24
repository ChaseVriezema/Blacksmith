using System;
using System.Collections.Generic;
using UnityEngine;


public class BlackjackController : GameController, BlackjackPresenter.IBlackjackPresenterController
{
    public interface IBlackjackPresenter
    {
        public void UpdateScore(Player player, int score);

        public void DrawCardForPlayer(CardBase card, GameController.Player player, bool faceUp = true);

        public void DisplayMessage(string message);

        public void StandForPlayer(GameController.Player player);

        public void RevealPlayerHand(GameController.Player player);

        public void ShowResetButton();

        public void ClearBoard();

        public Action TurnAnimationComplete { get; set; }
    }

    public interface IBlackjackModel
    {
        public DeckController Deck { get; }
        public HandController PlayerHand { get; }
        public HandController DealerHand { get; }

        bool GetPlayerIsStanding(Player player);
        bool GetEveryPlayerIsStanding();
        void StandPlayer(Player player);
        public void AddCard(GameController.Player player, CardBase card, bool faceUp = true);
        public List<PlayingCard> GetCardsInHand (GameController.Player player);
    }

    public class BlackjackGameConfig : GameConfig
    {
        public int AlwaysHitBelow = 17;
    }

    private BlackjackGameConfig blackjackConfig => (BlackjackGameConfig) gameConfig;
    private IBlackjackModel blackjackModel;
    private IBlackjackPresenter blackjackPresenter;
    private Player currentTurn;

    public BlackjackController(BlackjackGameConfig config, IBlackjackModel model, BlackjackPresenter presenter ) : base(config)
    {
        blackjackModel = model;
        blackjackPresenter = presenter;
    }

    public override void StartGame()
    {
        CreateDeck();
        blackjackModel.Deck.Shuffle();
        DealCards();
        currentTurn = Player.Player;
    }

    public void CompleteGame(Player winner)
    {
        blackjackModel.DealerHand.RevealHand();
        blackjackPresenter.RevealPlayerHand(Player.Dealer);
        UpdateScores();
        blackjackPresenter.DisplayMessage($"{winner} Wins!");
        blackjackPresenter.ShowResetButton();
    }

    public void EndGame()
    {
        OnComplete();
        blackjackPresenter.ClearBoard();
    }

    public void DoTurn(Player player)
    {
        if(blackjackModel.GetPlayerIsStanding(player))
            EndTurn();

        if(player == Player.Dealer)
        {
            var currentHandTotal = CalculateTotal(Player.Dealer, true);
            if(currentHandTotal < blackjackConfig.AlwaysHitBelow)
            {
                HitForPlayer(Player.Dealer);
            }
            else
            {
                StandForPlayer(Player.Dealer);
            }
        }
    }

    public void EndTurn()
    {
        blackjackPresenter.TurnAnimationComplete -= EndTurn;
        if(CheckForGameOver())
        {
            CompleteGame(GetWinner());
        }
        else
        {
            currentTurn = currentTurn == Player.Player ? Player.Dealer : Player.Player;
            DoTurn(currentTurn);
        }
    }

    public void CreateDeck()
    {
        for (int i = 0; i < 52; i++)
        {
            var newCard = CardFactory.CreatePlayingCard((i % 13), (i % 4));
            var added = blackjackModel.Deck.AddCardToTop(newCard);
            Debug.Log($"Added a {newCard.ToString()} to the deck. Successful = {added}.");
        }
    }

    public void DealCards()
    {
        DealToPlayer(Player.Player);
        DealToPlayer(Player.Dealer, false);
        DealToPlayer(Player.Player);
        DealToPlayer(Player.Dealer);
        UpdateScores();
    }

    public void DealToPlayer(Player player, bool faceUp = true)
    {
        var drawCard = blackjackModel.Deck.DrawCard() as PlayingCard;
        Debug.Log($"Drew the {drawCard.ToString()} for {player}");
        blackjackModel.AddCard(player, drawCard, faceUp);
        blackjackPresenter.DrawCardForPlayer(drawCard, player, faceUp);
    }

    public void HitForPlayer(Player player)
    {
        blackjackPresenter.TurnAnimationComplete += EndTurn;
        DealToPlayer(player);
        UpdateScores();
    }

    public void StandForPlayer(Player player)
    {
        blackjackPresenter.TurnAnimationComplete += EndTurn;
        blackjackModel.StandPlayer(player);
        blackjackPresenter.StandForPlayer(player);
    }

    public bool CheckForGameOver()
    {
        if(CalculateTotal(Player.Player) > 21 || CalculateTotal(Player.Dealer) > 21)
            return true;

        if(!blackjackModel.GetEveryPlayerIsStanding())
            return false;

        return true;
    }

    public Player GetWinner()
    {
        var playerTotal = CalculateTotal(Player.Player);
        var dealerTotal = CalculateTotal(Player.Dealer);

        if(playerTotal <= 21 && (dealerTotal > 21 || playerTotal > dealerTotal))
            return Player.Player;

        return Player.Dealer;
    }

    public void UpdateScores()
    {
        blackjackPresenter.UpdateScore(Player.Player, CalculateTotal(Player.Player));
        blackjackPresenter.UpdateScore(Player.Dealer, CalculateTotal(Player.Dealer));
    }

    public int CalculateTotal(Player player, bool includeFaceDown = false)
    {
        var cards = blackjackModel.GetCardsInHand(player);

        int cardSum = 0;
        int numberOfAces = 0;
        string logString = $"{player}'s Hand Total of ";

        foreach(CardBase card in cards)
        {
            logString += $"{card.ToString()}, ";
            var playingCard = card as PlayingCard;
            if(!includeFaceDown && !playingCard.FaceUp) continue;
            if(playingCard.CardValue == PlayingCard.PlayingCardValue.Ace)
            {
                numberOfAces ++;
            }
            else
            {
                cardSum += Mathf.Min(10, (int)playingCard.CardValue + 1);
            }
        }

        int total;
        if(numberOfAces == 0)
            total = cardSum;
        else
            total = cardSum <= 10 ? cardSum + 11 + (numberOfAces - 1) : cardSum + numberOfAces;

        Debug.Log($"{logString} is {total}.");
        return total;
    }
}


