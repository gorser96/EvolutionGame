namespace Domain.Models;

public class Addition
{
    public Addition(Guid uid, string name)
    {
        Uid = uid;
        Name = name;
        Cards = new List<Card>();
    }

    public Guid Uid { get; private set; }

    public string Name { get; private set; }

    public virtual ICollection<Card> Cards { get; private set; }
}
