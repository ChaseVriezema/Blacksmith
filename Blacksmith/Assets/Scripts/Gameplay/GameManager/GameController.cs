using System;

public abstract class GameController 
{
    public enum Player { Player, Dealer }
    
    public class GameConfig
    {
        public enum GameType { Blackjack, MultiBlackjack, RiggedBlackjack }
        public GameType Type;
    }

    protected GameConfig gameConfig;
    
    public GameController (GameConfig config)
    {
        gameConfig = config;
        
    }

    public Action OnComplete;
    public abstract void StartGame();
    
}
