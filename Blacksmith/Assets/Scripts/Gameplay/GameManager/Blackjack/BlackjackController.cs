using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlackjackController : GameController, BlackjackPresenter.IBlackjackPresenterController
{
    public interface IBlackjackPresenter
    {
        public void UpdateScore(Player player, int score);

        public void DrawCardForPlayer(CardBase card, GameController.Player player, bool faceUp = true);

        public void DisplayMessage(string message, float duration);

        public void StandForPlayer(GameController.Player player);

        public void RevealPlayerHand(GameController.Player player);

        public void ClearBoard();
    }

    public interface IBlackjackModel
    {
        public DeckController Deck { get; }
        public HandController PlayerHand { get; }
        public HandController DealerHand { get; }
        bool[] IsStanding { get; }
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

    public override IEnumerator InitGame()
    {
        CreateDeck();
        blackjackModel.Deck.Shuffle();
        DealCards();
        currentTurn = Player.Player;
        RunGame();
        yield return 0;
    }

    public override IEnumerator RunGame()
    {
        bool gameOver = false;
        while(!gameOver)
        {
            if(blackjackModel.IsStanding[(int)currentTurn])
                NextTurn();
            
            if(currentTurn == Player.Player)
            {
                Debug.Log("Awaiting Player Draw");
                yield return new WaitUntil(() => currentTurn != Player.Player);
            }
            else
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

            gameOver = CheckForGameOver();
        }

        Debug.Log("Game Is Over");
    }

    public override IEnumerator CompleteGame()
    {
        blackjackModel.DealerHand.RevealHand();
        blackjackPresenter.RevealPlayerHand(Player.Dealer);
        UpdateScores();
        var winner  = GetWinner();
        blackjackPresenter.DisplayMessage($"{winner} Wins!", 9f);
        yield return new WaitForSeconds(10f);
        blackjackPresenter.ClearBoard();
    }

    public void CreateDeck()
    {
        for (int i = 0; i < 52; i++)
        {
            var newCard = new PlayingCard((i % 13), (i % 4));
            var added = blackjackModel.Deck.AddCardToTop(newCard);
            Debug.Log($"Added a {newCard.ToString()} to the deck. Successful = {added}.");
        }
    }

    public void DealCards()
    {
        HitForPlayer(Player.Player);
        HitForPlayer(Player.Dealer, false);
        HitForPlayer(Player.Player);
        HitForPlayer(Player.Dealer);
    }

    public void HitForPlayer(Player player, bool faceUp = true)
    {
        var drawCard = blackjackModel.Deck.DrawCard() as PlayingCard;
        Debug.Log($"Drew the {drawCard.ToString()} for {player}");
        blackjackModel.AddCard(player, drawCard, faceUp);
        CalculateTotal(player);

        blackjackPresenter.DrawCardForPlayer(drawCard, player, faceUp);
        UpdateScores();
        NextTurn();
    }

    public void StandForPlayer(Player player)
    {
        blackjackModel.IsStanding[(int)player] = true;
        NextTurn();
    }

    public void NextTurn()
    {
        currentTurn = currentTurn == Player.Player ? Player.Dealer : Player.Player;
    }

    public bool CheckForGameOver()
    {
        if(CalculateTotal(Player.Player) > 21 || CalculateTotal(Player.Dealer) > 21)
            return true;

        if(blackjackModel.IsStanding.Any(p => p == false))
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

public class BlackjackModel : BlackjackController.IBlackjackModel
{
    public DeckController Deck { get; private set; }

    private HandController[] hands;
    public HandController PlayerHand => hands[(int)GameController.Player.Player];
    public HandController DealerHand => hands[(int)GameController.Player.Player];

    private bool[] isStanding;
    public bool[] IsStanding => isStanding;

    public BlackjackModel() 
    {
        Deck = new DeckController();
        hands = new HandController[2];
        for(int i = 0; i < hands.Length; i++)
        {
            hands[i] = new HandController((GameController.Player)i);
        }
        isStanding = new bool[2]{false, false};
    }

    public void StandPlayer(GameController.Player player)
    {
        isStanding[(int)player] = true;
    }

    public void AddCard(GameController.Player player, CardBase card, bool faceUp = true)
    {
        hands[(int) player].AddCard(card, faceUp);
    }

    public List<PlayingCard> GetCardsInHand (GameController.Player player)
    {
        return hands[(int) player].GetHeldCards().Cast<PlayingCard>().ToList();
    }


}
