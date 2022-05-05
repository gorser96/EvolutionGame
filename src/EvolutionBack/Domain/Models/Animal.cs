using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Animal
{
    public Animal(Guid uid, Guid inGameUserUserUid, Guid inGameUserRoomUid, Guid inGameCardUid)
    {
        Uid = uid;
        FoodCurrent = 0;
        FoodMax = 1;
        State = AnimalState.Alive;
        Properties = new List<InAnimalProperty>();
        InGameUserUserUid = inGameUserUserUid;
        InGameUserRoomUid = inGameUserRoomUid;
        InGameCardUid = inGameCardUid;
    }

    #region DTO properties

    public Guid Uid { get; private set; }

    public Guid InGameUserUserUid { get; init; }

    public Guid InGameUserRoomUid { get; init; }

    public Guid InGameCardUid { get; init; }

    public virtual InGameUser? InGameUser { get; private set; }

    public virtual InGameCard? InGameCard { get; private set; }

    public int FoodCurrent { get; private set; }

    public int FoodMax { get; private set; }

    public AnimalState State { get; private set; }

    public virtual ICollection<InAnimalProperty> Properties { get; private set; }

    #endregion DTO properties

    public void Reset()
    {
        SetFood(0);
        State = AnimalState.Alive;
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

    internal void AddState(AnimalState state)
    {
        State |= state;
    }

    private void SetMaxFood(int value)
    {
        FoodMax = value;
    }

    private void SetFood(int value)
    {
        FoodCurrent = value;
        if (FoodCurrent >= FoodMax)
        {
            AddState(AnimalState.Feeded);
        }
    }

    private bool CanAttack(Animal enemy, IDictionary<Guid, IPropertyAction> enemyProperties)
    {
        if (!enemyProperties.Any())
        {
            return true;
        }

        return enemyProperties.Values.All(x => x.CanAttack(enemy, this));
    }

    public (bool isSuccessAttack, IList<Guid>? activeDefenseProperties) Attack(Animal enemy)
    {
        // отбираем только пассивную и активную защиту
        var enemyProperties = enemy.Properties
            .ToDictionary(x => x.PropertyUid, x => x.GetPropertyAction())
            .Where(x => x.Value.PropertyType == AnimalPropertyType.PassiveDefense || x.Value.PropertyType == AnimalPropertyType.ActiveDefense)
            .ToDictionary(x => x.Key, x => x.Value);

        if (CanAttack(enemy, enemyProperties))
        {
            var activeDefenseList = enemyProperties.Where(x => x.Value.PropertyType == AnimalPropertyType.ActiveDefense);
            if (activeDefenseList.Any())
            {
                return (false, activeDefenseList.Select(x => x.Key).ToList());
            }

            return (true, null);
        }
        return (false, null);
    }

    public DefenseResult Defense(Animal attacker, Guid propertyUid, Guid? targetUid)
    {
        var defensiveProperty = Properties.FirstOrDefault(x => x.PropertyUid == propertyUid);
        if (defensiveProperty is null)
        {
            throw new ValidationException($"Property [uid={propertyUid}] not found in animal [uid={Uid}]!");
        }

        return defensiveProperty.GetPropertyAction().OnDefense(this, attacker, targetUid);
    }
}

[Flags]
public enum AnimalState
{
    None = 0,
    Alive = 1,
    Feeded = 2,
    Poisoned = 4,
    Sleeping = 8,
    InShell = 16,
    InShelter = 32,
}