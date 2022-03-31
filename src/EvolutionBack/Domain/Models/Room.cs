namespace EvolutionBack.Domain.Models;

public class Room
{
    public Room(Guid uid, string name)
    {
        Uid = uid;
        Name = name;
        Users = new List<User>();
    }

    public Guid Uid { get; set; }

    public string Name { get; set; }

    public virtual ICollection<User> Users { get; set; }
}
