namespace Domain.Models;

public class Animal
{
    public Animal(Guid uid)
    {
        Uid = uid;
        Properties = new List<Property>();
    }

    #region DTO properties

    public Guid Uid { get; private set; }

    public virtual InGameUser? InGameUser { get; private set; }

    public int FoodCurrent { get; private set; }

    public int FoodMax { get; private set; }

    public virtual ICollection<Property> Properties { get; private set; }

    #endregion DTO properties

    public void AddProperty(Property property)
    {
        // TODO: проверка совместимости свойств
        Properties.Add(property);
    }

    public void EnableProperties(IReadOnlyCollection<Guid> disabledPropertiesOnAttack)
    {
        foreach (var property in Properties.Where(x => disabledPropertiesOnAttack.Contains(x.Uid)))
        {
            if (property is IPropertyAction propertyAction)
            {
                propertyAction.SetIsActive(true);
            }
        }
    }

    public void DisableProperties(IReadOnlyCollection<Guid> disabledPropertiesOnAttack)
    {
        foreach (var property in Properties.Where(x => disabledPropertiesOnAttack.Contains(x.Uid)))
        {
            if (property is IPropertyAction propertyAction)
            {
                propertyAction.SetIsActive(false);
            }
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
        return !enemy.Properties.Cast<IPropertyAction>().All(x => x.OnDefense(this, enemy) ?? false);
    }
}
