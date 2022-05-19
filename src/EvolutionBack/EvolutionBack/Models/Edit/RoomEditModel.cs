namespace EvolutionBack.Models;

public class RoomEditModel
{
    public RoomEditModel(
        string name, TimeSpan? maxTimeLeft = null,
        ICollection<Guid>? additions = null, bool isPrivate = false, int numOfCards = 0)
    {
        Name = name;
        MaxTimeLeft = maxTimeLeft;
        Additions = additions;
        IsPrivate = isPrivate;
        NumOfCards = numOfCards;
    }

    public string Name { get; init; }

    public TimeSpan? MaxTimeLeft { get; init; }

    public ICollection<Guid>? Additions { get; init; }

    public bool IsPrivate { get; init; }

    public int NumOfCards { get; init; }
}
