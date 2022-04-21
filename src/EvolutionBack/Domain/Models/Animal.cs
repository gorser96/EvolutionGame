using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Animal
{
    public Animal(Guid uid, Guid inGameUserUserUid, Guid inGameUserRoomUid)
    {
        Uid = uid;
        FoodCurrent = 0;
        FoodMax = 1;
        Properties = new List<InAnimalProperty>();
        InGameUserUserUid = inGameUserUserUid;
        InGameUserRoomUid = inGameUserRoomUid;
    }

    #region DTO properties

    public Guid Uid { get; private set; }

    public Guid InGameUserUserUid { get; init; }

    public Guid InGameUserRoomUid { get; init; }

    public virtual InGameUser? InGameUser { get; private set; }

    public int FoodCurrent { get; private set; }

    public int FoodMax { get; private set; }

    public virtual ICollection<InAnimalProperty> Properties { get; private set; }

    #endregion DTO properties

    public void Reset()
    {
        SetFood(0);
    }

    public void Feed(int foodCount)
    {
        SetFood(FoodCurrent + foodCount);
    }

    public void AddProperty(Property property)
    {
        // TODO: проверка совместимости свойств
        Properties.Add(new InAnimalProperty(property.Uid, Uid));
        if (property.FeedIncreasing.HasValue && property.FeedIncreasing > 0)
        {
            SetMaxFood(FoodMax + property.FeedIncreasing.Value);
        }
    }

    public void RemoveProperty(Guid propertyUid)
    {
        var property = Properties.FirstOrDefault(x => x.AnimalUid == Uid && x.PropertyUid == propertyUid);
        if (property is null)
        {
            throw new ValidationException("Property for animal not found!");
        }

        Properties.Remove(property);
    }

    public void EnableProperties(IReadOnlyCollection<Guid> disabledPropertiesOnAttack)
    {
        foreach (var property in Properties.Where(x => disabledPropertiesOnAttack.Contains(x.PropertyUid)))
        {
            property.Update(true);
        }
    }

    public void DisableProperties(IReadOnlyCollection<Guid> disabledPropertiesOnAttack)
    {
        foreach (var property in Properties.Where(x => disabledPropertiesOnAttack.Contains(x.PropertyUid)))
        {
            property.Update(false);
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
        return !enemy.Properties.Select(x => x.GetPropertyAction()).All(x => x.OnDefense(this, enemy) ?? false);
    }
}
