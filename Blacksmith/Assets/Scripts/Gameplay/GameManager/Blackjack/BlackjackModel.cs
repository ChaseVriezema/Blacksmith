using System.Collections.Generic;
using System.Linq;

public class BlackjackModel : BlackjackController.IBlackjackModel
{
    public DeckController Deck { get; private set; }

    public GameController.Player currentTurn { get; set; }

    private Dictionary<GameController.Player, BlackjackPlayerModel> players;
    public HandController PlayerHand => players[GameController.Player.Player].Hand;
    public HandController DealerHand => players[GameController.Player.Dealer].Hand;

    public BlackjackModel() 
    {
        Deck = new DeckController();
        players = new Dictionary<GameController.Player, BlackjackPlayerModel>();

        for(int i = 0; i < 2; i++)
        {
            var playerType = (GameController.Player) i;
            players.Add(playerType,
                new BlackjackPlayerModel(playerType, new HandController()));
        }

    }

    public bool GetPlayerIsStanding(GameController.Player player)
    {
        return players[player].IsStanding;
    }

    public bool GetEveryPlayerIsStanding()
    {
        return !players.Any(p => p.Value.IsStanding == false);
    }

    public void StandPlayer(GameController.Player player)
    {
        players[player].StandForPlayer();
    }

    public void AddCard(GameController.Player player, CardBase card, bool faceUp = true)
    {
        players[player].Hand.AddCard(card, faceUp);
    }

    public List<PlayingCard> GetCardsInHand (GameController.Player player)
    {
        return players[player].Hand.GetHeldCards().Cast<PlayingCard>().ToList();
    }


}