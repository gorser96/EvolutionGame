namespace EvolutionBack.Models;

public class RoomEditModel
{
    public RoomEditModel(Guid uid, string name, TimeSpan? maxTimeLeft = null, ICollection<Guid>? additions = null)
    {
        Uid = uid;
        Name = name;
        MaxTimeLeft = maxTimeLeft;
        Additions = additions;
    }

    public Guid Uid { get; private set; }

    public string Name { get; private set; }

    public TimeSpan? MaxTimeLeft { get; private set; }

    public ICollection<Guid>? Additions { get; private set; }
}
