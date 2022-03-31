namespace Domain.Models;

public class Additional
{
    public Additional(Guid uid, string name)
    {
        Uid = uid;
        Name = name;
        AnimalProperties = new List<AnimalProperty>();
    }

    public Guid Uid { get; set; }

    public string Name { get; set; }

    public virtual ICollection<AnimalProperty> AnimalProperties { get; set; }
}
