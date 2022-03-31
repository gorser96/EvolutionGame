namespace Domain.Models;

public class Burrowing : IPropertyAction
{
    public Burrowing(Guid uid, string name, bool isPair, bool isOnEnemy, Guid additionalUid)
    {
        Uid = uid;
        Name = name;
        IsPair = isPair;
        IsOnEnemy = isOnEnemy;
        AdditionalUid = additionalUid;
    }

    public Guid Uid { get; set; }

    public string Name { get; set; }

    public bool IsPair { get; set; }

    public bool IsOnEnemy { get; set; }

    public Guid AdditionalUid { get; set; }

    public string AssemblyName => nameof(Burrowing);

    public void Invoke(Animal? self = null, Animal? target = null)
    {
    }
}
