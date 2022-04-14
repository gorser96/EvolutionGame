namespace EvolutionBack.Models;

public class RoomEditModel
{
    public RoomEditModel(string name, TimeSpan? maxTimeLeft = null, ICollection<Guid>? additions = null)
    {
        Name = name;
        MaxTimeLeft = maxTimeLeft;
        Additions = additions;
    }

    public string Name { get; private set; }

    public TimeSpan? MaxTimeLeft { get; private set; }

    public ICollection<Guid>? Additions { get; private set; }
}
