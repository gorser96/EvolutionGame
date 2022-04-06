namespace Domain.Models;

public class Animal
{
    public Animal(Guid uid)
    {
        Uid = uid;
        FoodCurrent = 0;
        FoodMax = 1;
        Properties = new List<Property>();
    }

    #region DTO properties

    public Guid Uid { get; private set; }

    public virtual InGameUser? InGameUser { get; private set; }

    public int FoodCurrent { get; private set; }

    public int FoodMax { get; private set; }

    public virtual ICollection<Property> Properties { get; private set; }

    #endregion DTO properties

    public void Update(int? foodCurrent = null, int? foodMax = null, InGameUser? user = null)
    {
        if (foodCurrent.HasValue)
        {
            SetFood(foodCurrent.Value);
        }

        if (foodMax.HasValue)
        {
            SetMaxFood(foodMax.Value);
        }

        if (user != null)
        {
            InGameUser = user;
        }
    }

    public void AddProperty(Property property)
    {
        // TODO: проверка совместимости свойств
        Properties.Add(property);
    }

    public void RemoveProperty(Property property)
    {
        Properties.Remove(property);
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

    private void SetMaxFood(int value)
    {
        FoodMax = value;
    }

    private void SetFood(int value)
    {
        FoodCurrent = value;
    }

    public bool Attack(Animal enemy)
    {
        return !enemy.Properties.Cast<IPropertyAction>().All(x => x.OnDefense(this, enemy) ?? false);
    }
}
