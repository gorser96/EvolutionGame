namespace Domain.Models;

public class Carnivorous : Property, IPropertyAction
{
    public Carnivorous(Guid uid, string name, bool isPair, bool isOnEnemy) 
        : base(uid, name, isPair, isOnEnemy, 1, nameof(Carnivorous))
    {
        IsActive = true;
    }

    public bool IsActive { get; private set; }

    public void SetIsActive(bool value)
    {
        IsActive = value;
    }

    public bool? OnDefense(Animal self, Animal enemy)
    {
        return null;
    }

    public void OnUse(Animal self, Animal? target = null)
    {
    }
}
