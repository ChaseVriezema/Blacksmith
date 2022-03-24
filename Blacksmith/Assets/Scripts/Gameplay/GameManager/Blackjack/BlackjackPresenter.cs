using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BlackjackPresenter : MonoBehaviour, BlackjackController.IBlackjackPresenter
{

    [SerializeField] private Button hitButton;
    [SerializeField] private Button standButton;

    [SerializeField] private Button resetButton;

    [SerializeField] private HandPresenter playerHand;
    [SerializeField] private HandPresenter dealerHand;

    [SerializeField] private Transform deckTransform;

    [SerializeField] private TextMeshProUGUI playerScore;
    [SerializeField] private TextMeshProUGUI dealerScore;

    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private GameObject cardPrefab;

    public Action TurnAnimationComplete { get; set; }

    public Action HitButtonPressed { get; set; }

    public Action StandButtonPressed { get; set; }

    public Action ResetButtonPressed { get; set; }

    public void Start()
    {
        hitButton.onClick.AddListener(() => HitButtonPressed());
        standButton.onClick.AddListener(() => StandButtonPressed());
        resetButton.onClick.AddListener(() => ResetButtonPressed());
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

    public void StandForPlayer(GameController.Player player)
    {
        //TODO: snazzy animation here
        TurnAnimationComplete();
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

    public void ShowResetButton()
    {
        DOTween.Sequence()
        .Append(hitButton.transform.DOScale(Vector3.zero, 0.33f).SetEase(Ease.OutSine))
        .Insert(0f, standButton.transform.DOScale(Vector3.zero, 0.33f).SetEase(Ease.OutSine))
        .AppendCallback(() => resetButton.gameObject.SetActive(true))
        .Append(resetButton.transform.DOScale(Vector3.zero, 0.33f).From().SetEase(Ease.OutSine));
    }

    public void ClearBoard()
    {
        //TODO: snazzy animation before destroy
        Destroy(this.gameObject);
    }

    public void DisplayMessage(string message)
    {
        messageText.text = message;
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

        seq.OnComplete(() => {
            if(TurnAnimationComplete != null) 
                TurnAnimationComplete();
        });
    }

    public GameObject CreateCardOnDeck(CardBase card)
    {
        var obj = Instantiate(cardPrefab, this.transform);
        cardPrefab.transform.position = deckTransform.position;
        obj.transform.Rotate(Vector3.up * 180.0f, Space.World);
        obj.GetComponent<PlayingCardPresenter>().Init(card as PlayingCard, true);

        return obj;
    }
}