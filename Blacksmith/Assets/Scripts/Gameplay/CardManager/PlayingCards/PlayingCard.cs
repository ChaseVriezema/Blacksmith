
public class PlayingCard : CardBase
{
   public enum PlayingCardValue {Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }

   public enum PlayingCardSuit {Air, Water, Earth, Fire}

   public PlayingCardValue CardValue { get; private set; }

   public PlayingCardSuit CardSuit { get; protected set; }
   

   public PlayingCard(int value, int suit)
   {
      CardValue = (PlayingCardValue)value;
      CardSuit = (PlayingCardSuit)suit;
   }

    public override string ToString()
    {
        return $"{CardValue} of {CardSuit}";
    }
}
