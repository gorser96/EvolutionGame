namespace Domain.Models;

/// <summary>
/// Игровое свойство
/// </summary>
public class Property
{
    public Property(Guid uid, string name, bool isPair, bool isOnEnemy, int? feedIncreasing, string assemblyName)
    {
        Uid = uid;
        Name = name;
        IsPair = isPair;
        IsOnEnemy = isOnEnemy;
        Animals = new List<InAnimalProperty>();

        AssemblyName = assemblyName;
        FeedIncreasing = feedIncreasing;
    }

    public Guid Uid { get; init; }

    /// <summary>
    /// Название свойства
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Полное имя класса свойства. Получается при помощи такой конструкции: <code>typeof(ИМЯ_СВОЙСТВА).FullName!</code><br/>
    /// Пример: <code>typeof(Burrowing).FullName!</code>
    /// </summary>
    public string AssemblyName { get; init; }

    /// <summary>
    /// Является ли свойство парным
    /// </summary>
    public bool IsPair { get; init; }

    /// <summary>
    /// Применяется ли свойство на животное другого игрока
    /// </summary>
    public bool IsOnEnemy { get; init; }

    /// <summary>
    /// Количество еды, на которое увеличивается потребность в еде у животного, к которому применяется это свойство
    /// </summary>
    public int? FeedIncreasing { get; init; }

    /// <summary>
    /// Активно ли свойство у животного
    /// </summary>
    /// <param name="selfAnimal"></param>
    /// <returns></returns>
    public bool IsActive(Animal animal)
    {
        return animal.Properties.Any(x => x.IsActive && x.Property.AssemblyName == AssemblyName);
    }

    public virtual ICollection<InAnimalProperty> Animals { get; private set; }
}
