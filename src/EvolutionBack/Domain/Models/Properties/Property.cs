namespace Domain.Models;

public class Property
{
    public Property(Guid uid, string name, bool isPair, bool isOnEnemy, string assemblyName)
    {
        Uid = uid;
        Name = name;
        IsPair = isPair;
        IsOnEnemy = isOnEnemy;
        Animals = new List<Animal>();

        AssemblyName = assemblyName;
    }

    public Guid Uid { get; private set; }

    public string Name { get; private set; }

    public string AssemblyName { get; init; }

    public bool IsPair { get; private set; }

    public bool IsOnEnemy { get; private set; }

    public virtual ICollection<Animal> Animals { get; private set; }
}
