namespace Domain.Models;

public class Property
{
    public Property(Guid uid, string name, bool isPair, bool isOnEnemy, int? feedIncreasing, string assemblyName)
    {
        Uid = uid;
        Name = name;
        IsPair = isPair;
        IsOnEnemy = isOnEnemy;
        Animals = new List<InAnimalProperty>();

        AssemblyName = assemblyName;
        FeedIncreasing = feedIncreasing;
    }

    public Guid Uid { get; init; }

    public string Name { get; private set; }

    public string AssemblyName { get; init; }

    public bool IsPair { get; init; }

    public bool IsOnEnemy { get; init; }

    public int? FeedIncreasing { get; init; }

    public virtual ICollection<InAnimalProperty> Animals { get; private set; }
}
