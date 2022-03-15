using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackController : GameController
{
    
    private BlackjackModel blackjackModel => (BlackjackModel) gameModel;
    private BlackjackPresenter blackjackPresenter => (BlackjackPresenter) gamePresenter;

    public BlackjackController( GameConfig config, BlackjackModel model, BlackjackPresenter presenter ) : base(config)
    {
        gameModel = model;
        gamePresenter = presenter;
    }


    public override void SetupBoard()
    {
        CreateDeck();
        blackjackModel.Deck.Shuffle();
        DealCards();
    }

    public override void CreateDeck()
    {
        for (int i = 0; i < 52; i++)
        {
            var newCard = new PlayingCard((i % 13), (i % 4));
            blackjackModel.Deck.AddCardToTop(newCard);
            Debug.Log($"Added a {newCard.CardValue} of {newCard.CardSuit} to the deck.");
        }
    }

    public override void DealCards()
    {
        HitForPlayer(Player.Player);
        HitForPlayer(Player.Dealer, false);
        HitForPlayer(Player.Player);
        HitForPlayer(Player.Dealer);
        UpdateScores();
    }

    public override void HitForPlayer(Player player, bool faceUp = true)
    {
        var drawCard = blackjackModel.Deck.DrawCard();
        blackjackModel.Hands[(int) player].AddCard(drawCard);
    }

    public override void StandForPlayer(Player player, bool faceUp = true)
    {
        
    }

    public override void UpdateScores()
    {
        throw new System.NotImplementedException();
    }

    public override int CalculateTotal()
    {
        throw new System.NotImplementedException();
    }
}

public class BlackjackPresenter : GameController.IGamePresenter
{
    
}

public class BlackjackModel : GameController.IGameModel
{
    public DeckController Deck;
    
    public DeckController DiscardPile;
    
    public HandController[] Hands;
}
