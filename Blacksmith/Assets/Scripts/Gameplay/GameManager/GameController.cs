using System.Collections;

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
    public abstract IEnumerator InitGame();
    public abstract IEnumerator RunGame();
    public abstract IEnumerator CompleteGame();
    
}
