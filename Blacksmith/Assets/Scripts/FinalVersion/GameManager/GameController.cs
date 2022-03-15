using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameController 
{
    public enum Player { Player, Dealer }

    public interface IGameModel
    {

    }

    public interface IGamePresenter
    {
        
    }
    
    public class GameConfig
    {
        public enum GameType { Blackjack, MultiBlackjack, RiggedBlackjack }

        public GameType Type;
        
    }

    protected GameConfig gameConfig;
    protected IGameModel gameModel;
    protected IGamePresenter gamePresenter;

    protected GameController(){}
    
    public GameController (GameConfig config)
    {
        gameConfig = config;
        
    }

    public abstract void SetupBoard();

    public abstract void CreateDeck();

    public abstract void DealCards();

    public abstract void HitForPlayer(Player player, bool faceUp = true);

    public abstract void StandForPlayer(Player player, bool faceUp = true);

    public abstract void UpdateScores();

    public abstract int CalculateTotal();
}
