namespace Domain.Models;

public class Room
{
    public Room(Guid uid, string name)
    {
        Uid = uid;
        Name = name;
        Users = new List<InGameUser>();
    }

    public Guid Uid { get; set; }

    public string Name { get; set; }

    public virtual ICollection<InGameUser> Users { get; set; }
}
