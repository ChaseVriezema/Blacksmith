using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class HandPresenter : MonoBehaviour
{
    private List<PlayingCardPresenter> cardsHeld;

    [SerializeField] private List<Transform> slots;

    public Transform GetNextSlot()
    {
        return cardsHeld.Count > slots.Count ? slots[cardsHeld.Count] : slots.Last();
    }

    public void AddCardToHand(PlayingCardPresenter cardPresenter, bool faceUp = true)
    {
        cardsHeld.Add(cardPresenter);

        for(int i = 0; i < cardsHeld.Count; i++)
        {
            var presenter = cardsHeld[i];
            presenter.transform.DOMove(slots[i].position, 0.33f).SetEase(Ease.OutSine);
        }
    }

    public void RevealAllCards()
    {
        foreach(var presenter in cardsHeld.Where(p => p.FaceUp == false))
        {
            presenter.transform.DORotate(Vector3.up * 180, 0.33f).SetEase(Ease.OutSine);
        }
    }
}
