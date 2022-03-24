using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BlackjackPresenter : MonoBehaviour, BlackjackController.IBlackjackPresenter
{

    public interface IBlackjackPresenterController
    {
        public void HitForPlayer(GameController.Player player, bool faceUp = true);
        public void StandForPlayer(GameController.Player player);
    }
    private IBlackjackPresenterController blackjackController;
    [SerializeField] private Button hitButton;
    [SerializeField] private Button standButton;

    [SerializeField] private HandPresenter playerHand;
    [SerializeField] private HandPresenter dealerHand;

    [SerializeField] private Transform deckTransform;

    [SerializeField] private TextMeshProUGUI playerScore;
    [SerializeField] private TextMeshProUGUI dealerScore;

    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private GameObject cardPrefab;

    public void InitBlackjackPresenter(IBlackjackPresenterController controller)
    {
        blackjackController = controller;
        hitButton.onClick.AddListener(PressHitButton);
        standButton.onClick.AddListener(PressStandButton);
        messageText.text = "";
    }

    public void UpdateScore(GameController.Player player, int total)
    {
        switch(player)
        {
            case GameController.Player.Player:
                playerScore.text = total.ToString();
                break;
            case GameController.Player.Dealer:
                dealerScore.text = total.ToString();
                break;
        }
    }

    public void PressHitButton()
    {
        blackjackController.HitForPlayer(GameController.Player.Player);
    }

    public void PressStandButton()
    {
        blackjackController.StandForPlayer(GameController.Player.Player);
    }

    public void StandForPlayer(GameController.Player player)
    {
        //TODO: snazzy animation here
    }

    public void RevealPlayerHand(GameController.Player player)
    {
        if(player == GameController.Player.Player)  
            playerHand.RevealAllCards();
        else 
            dealerHand.RevealAllCards();
    }

    public void ChangeCurrentTurn(GameController.Player player)
    {
        if(player == GameController.Player.Player)
        {
            hitButton.enabled = false;
            standButton.enabled = false;
        }
        else
        {
            hitButton.enabled = true;
            standButton.enabled = true;
        }
    }

    public void ClearBoard()
    {
        //TODO: snazzy animation before destroy
        Destroy(this.gameObject);
    }

    public void DisplayMessage(string message, float duration)
    {
        DOTween.Sequence()
        .AppendCallback(() => messageText.text = message)
        .AppendInterval(duration)
        .AppendCallback(() => messageText.text = "");
    }

    public void DrawCardForPlayer(CardBase card, GameController.Player player, bool faceUp = true)
    {
        var cardObj = CreateCardOnDeck(card);

        var slot = player == GameController.Player.Player ? 
            playerHand.AddCardToHand(cardObj.GetComponent<PlayingCardPresenter>()) : 
            dealerHand.AddCardToHand(cardObj.GetComponent<PlayingCardPresenter>());

        var seq = DOTween.Sequence();
        seq.Append(cardObj.transform.DOMove(slot.transform.position, 0.33f).SetEase(Ease.OutSine));
        seq.Insert(0.1f, cardObj.GetComponent<PlayingCardPresenter>().FlipCard(0.23f, faceUp));
    }

    public GameObject CreateCardOnDeck(CardBase card)
    {
        var obj = Instantiate(cardPrefab);
        cardPrefab.transform.position = deckTransform.position;
        obj.transform.Rotate(Vector3.up * 180.0f, Space.World);
        obj.GetComponent<PlayingCardPresenter>().Init(card as PlayingCard, true);

        return obj;
    }
}