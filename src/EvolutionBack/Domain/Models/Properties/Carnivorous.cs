namespace Domain.Models;

public class Carnivorous : IPropertyAction
{
    public Carnivorous(Guid uid, string name, bool isPair, bool isOnEnemy, Guid additionalUid)
    {
        Uid = uid;
        Name = name;
        IsPair = isPair;
        IsOnEnemy = isOnEnemy;
        AdditionalUid = additionalUid;

        IsActive = true;
    }

    public bool IsActive { get; set; }

    public Guid Uid { get; set; }

    public string Name { get; set; }

    public string AssemblyName => nameof(Carnivorous);

    public bool IsPair { get; set; }

    public bool IsOnEnemy { get; set; }

    public Guid AdditionalUid { get; set; }

    public void SetIsActive(bool value)
    {
        IsActive = value;
    }

    public bool? OnDefense(Animal self, Animal enemy)
    {
        return null;
    }

    public void OnUse(Animal self)
    {
    }
}
