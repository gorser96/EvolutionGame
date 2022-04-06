namespace Domain.Models;

public class Carnivorous : Property, IPropertyAction
{
    public Carnivorous(Guid uid, string name, bool isPair, bool isOnEnemy) 
        : base(uid, name, isPair, isOnEnemy, nameof(Carnivorous))
    {
        IsActive = true;
    }

    public bool IsActive { get; set; }

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
