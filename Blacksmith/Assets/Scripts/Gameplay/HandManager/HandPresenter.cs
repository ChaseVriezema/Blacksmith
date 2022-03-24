using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandPresenter : MonoBehaviour
{
    private List<PlayingCardPresenter> cardsHeld = new List<PlayingCardPresenter>();
    [SerializeField] private List<Transform> slots;

    public Transform GetNextSlot()
    {
        return cardsHeld.Count < slots.Count ? slots[cardsHeld.Count] : slots.Last();
    }

    public Transform AddCardToHand(PlayingCardPresenter cardPresenter)
    {
        var transform = GetNextSlot();
        cardsHeld.Add(cardPresenter);
        return transform;
    }

    public void RevealAllCards()
    {
        foreach(var presenter in cardsHeld.Where(p => p.FaceUp == false))
        {
            presenter.FlipCard(0.16f, true);
        }
    }
}
