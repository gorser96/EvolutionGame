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
        
        IsActive = true;
    }

    public Guid Uid { get; set; }

    public string Name { get; set; }

    public bool IsPair { get; set; }

    public bool IsOnEnemy { get; set; }

    public Guid AdditionalUid { get; set; }

    public string AssemblyName => nameof(Burrowing);

    public bool IsActive { get; set; }

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

    public void OnUse(Animal self)
    {
        // это свойство не активируется по требованию (например как "Спячка")
    }
}
