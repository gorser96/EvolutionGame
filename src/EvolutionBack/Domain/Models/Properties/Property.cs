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

    /// <summary>
    /// Активно ли свойство у животного
    /// </summary>
    /// <param name="selfAnimal"></param>
    /// <returns></returns>
    public bool IsActive(Animal animal)
    {
        return animal.Properties.Any(x => x.IsActive && x.Property.AssemblyName == AssemblyName);
    }

    public virtual ICollection<InAnimalProperty> Animals { get; private set; }
}
