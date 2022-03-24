public class BlackjackPlayerModel 
{
    public GameController.Player Player { get; private set; }
    public bool IsStanding { get; private set;}
    public HandController Hand { get; private set;}

    public BlackjackPlayerModel (GameController.Player player, HandController hand)
    {
        Player = player;
        Hand = hand;
        IsStanding = false;
    }

    public void StandForPlayer()
    {
        IsStanding = true;
    }
}
