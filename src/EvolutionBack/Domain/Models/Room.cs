namespace Domain.Models;

public class Room
{
    public Room(Guid uid, string name)
    {
        Uid = uid;
        Name = name;
        InGameUsers = new List<InGameUser>();
    }

    public Guid Uid { get; private set; }

    public string Name { get; private set; }

    public virtual ICollection<InGameUser> InGameUsers { get; private set; }
}
