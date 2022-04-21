namespace Domain.Models;

public class Burrowing : Property, IPropertyAction
{
    public Burrowing(Guid uid, string name, bool isPair, bool isOnEnemy) 
        : base(uid, name, isPair, isOnEnemy, 0, nameof(Burrowing))
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
        // если это свойство не заблокировано, например, "Неоплазией" или "Интеллектом"
        if (IsActive)
        {
            if (self.FoodCurrent >= self.FoodMax)
            {
                return true;
            }
        }

        return false;
    }

    public void OnUse(Animal self, Animal? target = null)
    {
        // это свойство не активируется по требованию (например как "Спячка")
    }
}
