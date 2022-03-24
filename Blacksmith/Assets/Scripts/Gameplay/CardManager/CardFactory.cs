
public static class CardFactory
{
    private static int prevId = 0;

    private static int GetUniqueId()
    {
        prevId++;
        return prevId;
    }

    public static PlayingCard CreatePlayingCard(PlayingCard.PlayingCardValue value, PlayingCard.PlayingCardSuit suit)
    {
        return new PlayingCard(GetUniqueId(), value, suit);
    }

    public static PlayingCard CreatePlayingCard(int value, int suit)
    {
        return CreatePlayingCard((PlayingCard.PlayingCardValue)value, (PlayingCard.PlayingCardSuit)suit);
    }
        
}
