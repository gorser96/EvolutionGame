namespace Domain.Models;

/// <summary>
/// Мимикрия
/// </summary>
public class Mimicry : Property, IPropertyAction
{
    public Mimicry(Guid uid, string name)
        : base(uid, name, isPair: false, isOnEnemy: false, feedIncreasing: 0, assemblyName: typeof(Mimicry).FullName!)
    {
    }

    public AnimalPropertyType PropertyType => AnimalPropertyType.ActiveDefense;

    public bool CanAttack(Animal self, Animal enemy)
    {
        return true;
    }

    public DefenseResult OnDefense(Animal self, Animal enemy, Guid? targetUid)
    {
        if (IsActive(self))
        {
            var targetAnimal = self.InGameUser?.Animals.FirstOrDefault(x => x.Uid == targetUid);
            if (targetAnimal is null)
            {
                return new(false);
            }

            return new(true, new(targetAnimal: targetAnimal, targetProperty: null));
        }

        return new(false);
    }

    public void OnUse(Animal self, Animal? target)
    {
    }
}
