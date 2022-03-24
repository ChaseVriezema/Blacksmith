using System.Collections.Generic;


public class HandController
{

    private List<CardBase> cardsHeld;
    public GameController.Player Owner { get; private set; }

    public HandController(GameController.Player owner)
    {
        cardsHeld = new List<CardBase>();
        Owner = owner;
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
}
