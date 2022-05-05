namespace Domain.Models;

/// <summary>
/// Норное
/// </summary>
public class Burrowing : Property, IPropertyAction
{
    public Burrowing(Guid uid, string name)
        : base(uid, name, isPair: false, isOnEnemy: false, feedIncreasing: 0, typeof(Burrowing).FullName!)
    {
    }

    public AnimalPropertyType PropertyType => AnimalPropertyType.PassiveDefense;

    public DefenseResult OnDefense(Animal self, Animal enemy, Guid? targetUid)
    {
        return new(false);
    }

    public void OnUse(Animal self, Animal? target = null)
    {
        // это свойство не активируется по требованию (например как "Спячка")
    }

    public bool CanAttack(Animal self, Animal enemy)
    {
        // если это свойство не заблокировано, например, "Неоплазией" или "Интеллектом"
        if (IsActive(self))
        {
            if (self.FoodCurrent >= self.FoodMax)
            {
                return false;
            }
        }

        return true;
    }
}
