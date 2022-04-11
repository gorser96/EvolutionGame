namespace Domain.Models;

public class RoomUpdateModel
{
    public RoomUpdateModel(string name, TimeSpan? maxTimeLeft, ICollection<Addition> additions)
    {
        Name = name;
        MaxTimeLeft = maxTimeLeft;
        Additions = additions;
    }

    public string Name { get; private set; }

    public TimeSpan? MaxTimeLeft { get; private set; }

    public ICollection<Addition> Additions { get; private set; }
}
