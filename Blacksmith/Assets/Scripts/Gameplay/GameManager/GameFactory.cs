using System;
using UnityEngine;

public static class GameFactory
{
    public static GameController CreateGame(GameController.GameConfig config)
    {
        switch(config.Type)
        {
            case GameController.GameConfig.GameType.Blackjack:
                return CreateBlackjack(config as BlackjackController.BlackjackGameConfig);
        }
        throw new NotImplementedException();
    }

    private static BlackjackController CreateBlackjack(BlackjackController.BlackjackGameConfig config)
    {
        var presenterObj = GameObject.Instantiate(GameSettings.Instance.BlackjackPresenter);
        var presenter = presenterObj.GetComponent<BlackjackPresenter>();
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