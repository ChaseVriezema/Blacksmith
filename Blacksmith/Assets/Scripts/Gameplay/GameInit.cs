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

    private void Reset()
    {
        StartButton.onClick.AddListener(InitBlackjack);
        StartButton.transform.DOScale(Vector3.one, 0.33f).SetEase(Ease.OutSine);
    }

    private void InitBlackjack()
    {
        StartButton.onClick.RemoveAllListeners();
        StartButton.transform.DOScale(Vector3.zero, 0.33f).SetEase(Ease.OutSine);
        runningGame = GameFactory.CreateGame(
            new BlackjackController.BlackjackGameConfig
            {
                Type = this.Type,
                AlwaysHitBelow = 17
            }
        );
        runningGame.OnComplete += Reset;
        runningGame.StartGame();
    }

}
