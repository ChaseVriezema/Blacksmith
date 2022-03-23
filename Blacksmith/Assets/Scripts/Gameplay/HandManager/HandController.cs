using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController
{

    private List<CardBase> cardsHeld;
    private GameController.Player owner;
    public GameController.Player Owner => owner;

    public HandController(GameController.Player owner)
    {
        cardsHeld = new List<CardBase>();
        this.owner = owner;
    }

    public bool AddCard(CardBase card, bool faceUp = true)
    {
        card.FaceUp = faceUp;
        cardsHeld.Add(card);
        return true;
    }

    public void RevealHand()
    {
        foreach(CardBase card in cardsHeld)
        {
            card.FaceUp = true;
        }
    }

    public CardBase RemoveCard(CardBase card)
    {
        cardsHeld.Remove(card);
        return card;
    }

    public List<CardBase> RemoveAllCards()
    {
        var retList = cardsHeld;
        cardsHeld.Clear();
        return retList;
    }

    public CardBase[] GetHeldCards()
    {
        return cardsHeld.ToArray();
    }

    public int CalculateHandTotal(bool includeFaceDown = false)
    {
        int cardSum = 0;
        int numberOfAces = 0;
        string logString = $"{owner}'s Hand Total of ";
        foreach(CardBase card in cardsHeld)
        {
            logString += $"{card.ToString()}, ";
            var playingCard = card as PlayingCard;
            if(!includeFaceDown && !playingCard.FaceUp) continue;
            if(playingCard.CardValue == PlayingCard.PlayingCardValue.Ace)
            {
                numberOfAces ++;
            }
            else
            {
                cardSum += Mathf.Min(10, (int)playingCard.CardValue + 1);
            }
        }
        int total;
        if(numberOfAces == 0)
            total = cardSum;
        else
            total = cardSum <= 10 ? cardSum + 11 + (numberOfAces - 1) : cardSum + numberOfAces;
        Debug.Log($"{logString} is {total}.");
        return total;
    }
}
