using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory
{
    public static GameController CreateGame(GameController.GameConfig config)
    {
        switch(config.Type)
        {
            case GameController.GameConfig.GameType.Blackjack:
                return CreateBlackjack(config as BlackjackController.BlackjackGameConfig);
        }
        return null;
    }

    private static BlackjackController CreateBlackjack(BlackjackController.BlackjackGameConfig config)
    {
        var presenter = GameObject.Instantiate(GameSettings.Instance.BlackjackPresenter.gameObject).GetComponent<BlackjackPresenter>();
        var model = new BlackjackModel();
        
        var controller = new BlackjackController(
                    config,
                    model,
                    presenter
                );
                
        presenter.InitBlackjackPresenter(controller);

        return controller;
    }
}