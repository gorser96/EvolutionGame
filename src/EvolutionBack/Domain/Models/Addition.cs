namespace Domain.Models;

public class Addition
{
    public Addition(Guid uid, string name)
    {
        Uid = uid;
        Name = name;
        Properties = new List<IProperty>();
    }

    public Guid Uid { get; set; }

    public string Name { get; set; }

    public virtual ICollection<IProperty> Properties { get; set; }
}
