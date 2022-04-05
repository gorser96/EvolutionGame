namespace Domain.Models;

public class Animal
{
    public Animal(Guid uid, Guid userUid)
    {
        Uid = uid;
        InGameUserUid = userUid;
        Properties = new List<IPropertyAction>();
    }

    #region DTO properties

    public Guid Uid { get; private set; }

    public Guid InGameUserUid { get; private set; }

    public int FoodCurrent { get; private set; }

    public int FoodMax { get; private set; }

    public virtual ICollection<IPropertyAction> Properties { get; private set; }

    #endregion DTO properties

    public void AddProperty(IPropertyAction property)
    {
        // TODO: проверка совместимости свойств
        Properties.Add(property);
    }

    public void EnableProperties(IReadOnlyCollection<Guid> disabledPropertiesOnAttack)
    {
        foreach (var property in Properties.Where(x => disabledPropertiesOnAttack.Contains(x.Uid)))
        {
            property.SetIsActive(true);
        }
    }

    public void DisableProperties(IReadOnlyCollection<Guid> disabledPropertiesOnAttack)
    {
        foreach (var property in Properties.Where(x => disabledPropertiesOnAttack.Contains(x.Uid)))
        {
            property.SetIsActive(false);
        }
    }

    public void SetMaxFood(int value)
    {
        FoodMax = value;
    }

    public void SetFood(int value)
    {
        FoodCurrent = value;
    }

    public bool Attack(Animal enemy)
    {
        return !enemy.Properties.All(x => x.OnDefense(this, enemy) ?? false);
    }
}
