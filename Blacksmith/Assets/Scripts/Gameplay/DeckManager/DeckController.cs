using System;
using System.Collections.Generic;

public class DeckController 
{
   private List<CardBase> deck = new List<CardBase>();
   public bool IsEmpty => deck.Count == 0;
   
   public void Shuffle()
   {
      for (int i = 0; i < deck.Count; i++) {
         var temp = deck[i];
         int randomIndex = UnityEngine.Random.Range(i, deck.Count);
         deck[i] = deck[randomIndex];
         deck[randomIndex] = temp;
      }
   }

   public CardBase DrawCard()
   {
      return DrawCardAt(0);
   }

   public CardBase DrawCardAt(int i)
   {
      if(i < deck.Count)
      {
         var drawnCard = deck[i];
         deck.Remove(drawnCard);
         return drawnCard;
      }
      throw new IndexOutOfRangeException($"Cannot draw card at {i}. Deck does not have index {i}.");
   }

   public bool AddCardAt(CardBase card, int i)
   {
      if (i >= 0 && i <= deck.Count)
      {
         deck.Insert(i, card);
         return true;
      }
      throw new IndexOutOfRangeException($"Cannot add card at {i}. Deck does not have index {i}.");
   }

   public bool AddCardToBottom(CardBase card)
   {
      deck.Add(card);
      return true;
   }

   public bool AddCardToTop(CardBase card)
   {
      return AddCardAt(card, 0);
   }
}
