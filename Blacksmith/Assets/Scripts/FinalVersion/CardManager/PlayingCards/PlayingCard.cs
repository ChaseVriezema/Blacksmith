using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingCard : CardBase
{
   public enum PlayingCardValue {Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }

   public enum PlayingCardSuit {Air, Water, Earth, Fire}

   private PlayingCardValue cardValue;
   private PlayingCardSuit cardSuit;

   public PlayingCardValue CardValue => cardValue;

   public PlayingCardSuit CardSuit => cardSuit;
   

   public PlayingCard(int value, int suit)
   {
      cardValue = (PlayingCardValue)value;
      cardSuit = (PlayingCardSuit)suit;
   }

    public override string ToString()
    {
        return $"{CardValue} of {CardSuit}";
    }
}
