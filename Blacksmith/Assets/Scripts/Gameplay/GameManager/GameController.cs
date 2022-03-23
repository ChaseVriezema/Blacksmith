using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all games, takes a GameConfig that has all game specific settings.
/// GameConfig will be loaded from json or scriptable object in the future.
/// </summary>
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
