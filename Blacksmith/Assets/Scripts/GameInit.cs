using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameInit : MonoBehaviour
{
    [SerializeField] private Button StartButton;

    [SerializeField] private GameController.GameConfig.GameType Type;

    private GameController runningGame;
    void Start()
    {
        StartButton.onClick.AddListener(InitBlackjack);
    }

    //Would typically be done by a proper factory pattern
    private void InitBlackjack()
    {
        StartButton.onClick.RemoveAllListeners();
        StartButton.transform.DOScale(Vector3.zero, 0.33f).SetEase(Ease.OutSine);
        switch(Type)
        {
            case GameController.GameConfig.GameType.Blackjack:
                BlackjackPresenter presenter = GameObject.Instantiate(GameSettings.Instance.BlackjackPresenter.gameObject).GetComponent<BlackjackPresenter>();
                BlackjackModel model = new BlackjackModel();
                BlackjackController controller = new BlackjackController(
                    new GameController.GameConfig{
                        Type = GameController.GameConfig.GameType.Blackjack
                    },
                    model,
                    presenter
                );

                runningGame = controller;
                break;
        }

        runningGame.SetupBoard();

    }

}
