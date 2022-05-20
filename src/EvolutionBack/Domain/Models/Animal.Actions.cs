using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

/// <summary>
/// Описание действий животного
/// </summary>
public partial class Animal
{
    /// <summary>
    /// Сброс животного на состояние фазы развития
    /// </summary>
    public void Reset()
    {
        SetFood(0);
        State = AnimalState.Alive;
    }

    /// <summary>
    /// Покормить животное
    /// </summary>
    /// <param name="foodCount"></param>
    public void Feed(int foodCount)
    {
        SetFood(FoodCurrent + foodCount);
    }

    /// <summary>
    /// Добавить свойство животному
    /// </summary>
    /// <param name="property"></param>
    public void AddProperty(Property property)
    {
        // TODO: проверка совместимости свойств
        Properties.Add(new InAnimalProperty(property.Uid, Uid));
        if (property.FeedIncreasing.HasValue && property.FeedIncreasing > 0)
        {
            SetMaxFood(FoodMax + property.FeedIncreasing.Value);
        }
    }

    /// <summary>
    /// Удалить свойство у животного
    /// </summary>
    /// <param name="propertyUid"></param>
    /// <exception cref="ValidationException"></exception>
    public void RemoveProperty(Guid propertyUid)
    {
        var property = Properties.FirstOrDefault(x => x.AnimalUid == Uid && x.PropertyUid == propertyUid);
        if (property is null)
        {
            throw new ValidationException("Property for animal not found!");
        }

        Properties.Remove(property);
    }

    /// <summary>
    /// Включить свойства животного
    /// </summary>
    /// <param name="disabledPropertiesOnAttack"></param>
    public void EnableProperties(IReadOnlyCollection<Guid> disabledPropertiesOnAttack)
    {
        foreach (var property in Properties.Where(x => disabledPropertiesOnAttack.Contains(x.PropertyUid)))
        {
            property.Update(true);
        }
    }

    /// <summary>
    /// Выключить свойства животного
    /// </summary>
    /// <param name="disabledPropertiesOnAttack"></param>
    public void DisableProperties(IReadOnlyCollection<Guid> disabledPropertiesOnAttack)
    {
        foreach (var property in Properties.Where(x => disabledPropertiesOnAttack.Contains(x.PropertyUid)))
        {
            property.Update(false);
        }
    }

    /// <summary>
    /// Добавить состояние животному
    /// </summary>
    /// <param name="state"></param>
    internal void AddState(AnimalState state)
    {
        State |= state;
    }

    /// <summary>
    /// Проверка возможности напасть на противника
    /// </summary>
    /// <param name="enemy">противник</param>
    /// <param name="enemyProperties">Список защитных свойств противника</param>
    /// <returns></returns>
    private bool CanAttack(Animal enemy, IDictionary<Guid, IPropertyAction> enemyProperties)
    {
        if (!enemyProperties.Any())
        {
            return true;
        }

        return enemyProperties.Values.All(x => x.CanAttack(enemy, this));
    }

    /// <summary>
    /// Команда нападения на другое животное
    /// </summary>
    /// <param name="enemy">Другое животное</param>
    /// <returns>isSuccessAttack - успешно ли нападение<br/>
    /// activeDefenseProperties - список активной защиты противника</returns>
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

    /// <summary>
    /// Команда защиты животного от нападающего, посредством выбранного свойства
    /// </summary>
    /// <param name="attacker">Нападающее животное</param>
    /// <param name="propertyUid">Защитное свойство</param>
    /// <param name="targetUid">Цель защитного свойства, может быть другим животным или свойством</param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
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
