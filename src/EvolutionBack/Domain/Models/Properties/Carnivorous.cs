namespace Domain.Models;

/// <summary>
/// Хищное 
/// </summary>
public class Carnivorous : Property, IPropertyAction
{
    public Carnivorous(Guid uid, string name) 
        : base(uid, name, isPair: false, isOnEnemy: false, feedIncreasing: 1, typeof(Carnivorous).FullName!)
    {
    }

    public AnimalPropertyType PropertyType => AnimalPropertyType.Passive;

    public DefenseResult OnDefense(Animal self, Animal enemy, Guid? targetUid)
    {
        return new(false);
    }

    public void OnUse(Animal self, Animal? target = null)
    {
    }

    public bool CanAttack(Animal self, Animal enemy)
    {
        return true;
    }
}
