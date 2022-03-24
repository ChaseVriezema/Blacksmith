public abstract class CardBase
{
    public int Id { get; protected set; }
    public bool FaceUp { get; set; }

    public CardBase (int id)
    {
        Id = id;
    }

}
