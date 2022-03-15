using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController
{

    public interface IHandControllerPresenter
    {
        
    }
    
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
        cardsHeld.Add(card);
        return true;
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
}
