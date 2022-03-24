using System.Collections;
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

    /// <summary>
    /// Initializes Blackjack game and startings running it via coroutine
    /// </summary>
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
        StartCoroutine(RunGame(runningGame));
    }

    /// <summary>
    /// Coroutine that will go through each step of a game controllere and call scene reset
    /// </summary>
    /// <param name="gameToRun">Game controller that will be run</param>
    /// <returns></returns>
    public IEnumerator RunGame(GameController gameToRun)
    {
        Debug.Log("InitGame");
        yield return gameToRun.InitGame();
        Debug.Log("RunGame");
        yield return gameToRun.RunGame();
        Debug.Log("CompleteGame");
        yield return gameToRun.CompleteGame();
        Debug.Log("Game Done");
        Reset();
    }

}
