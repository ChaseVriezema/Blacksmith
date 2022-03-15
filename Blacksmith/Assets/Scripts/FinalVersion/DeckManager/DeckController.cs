using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
   private List<CardBase> cardHolder;

   public bool IsEmpty => cardHolder.Count == 0;

   public DeckController()
   {
      cardHolder = new List<CardBase>();
   }
   
   public void Shuffle()
   {
      
   }

   public CardBase DrawCard()
   {
      return DrawCardAt(0);
   }

   public CardBase DrawCardAt(int i)
   {
      return i < cardHolder.Count ? cardHolder[0] : null;
   }

   public bool AddCardAt(CardBase card, int i)
   {
      if (i < cardHolder.Count)
      {
         cardHolder.Insert(i, card);
         return true;
      }

      return false;
   }

   public bool AddCardToBottom(CardBase card)
   {
      cardHolder.Add(card);
      return true;
   }

   public bool AddCardToTop(CardBase card)
   {
      return AddCardAt(card, 0);
   }
}
