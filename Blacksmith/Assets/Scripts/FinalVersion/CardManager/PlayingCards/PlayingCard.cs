using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingCard : CardBase
{
   public enum PlayingCardValue {Ace, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }

   private PlayingCardValue cardValue;

   public PlayingCardValue CardValue => cardValue;
   
   public enum PlayingCardSuit {Air, Water, Earth, Fire}

   private PlayingCardSuit cardSuit;

   public PlayingCardSuit CardSuit => cardSuit;

   public PlayingCard(int value, int suit)
   {
      cardValue = (PlayingCardValue)value;
      cardSuit = (PlayingCardSuit)suit;
   }
}
