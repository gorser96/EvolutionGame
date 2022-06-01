using System.Collections.Concurrent;
using System.Reflection;

namespace Domain.Models;

/// <summary>
/// Свойсто, которое присвоено животному
/// </summary>
public class InAnimalProperty
{
    private static readonly Assembly _assembly = Assembly.GetAssembly(typeof(Property)) ?? Assembly.GetExecutingAssembly();
    private readonly object _lock = new();

    private static ConcurrentDictionary<Guid, IPropertyAction> _propertyActionCache = new();

#pragma warning disable CS8618
    public InAnimalProperty(Guid propertyUid, Guid animalUid)
    {
        PropertyUid = propertyUid;
        AnimalUid = animalUid;
        IsActive = true;
    }
#pragma warning restore CS8618

    public Guid PropertyUid { get; init; }

    public virtual Property Property { get; init; }

    public Guid AnimalUid { get; init; }

    public virtual Animal Animal { get; init; }

    public bool IsHasFatTissue { get; private set; }

    public bool IsActive { get; private set; }

    public void Update(bool? isActive = null, bool? isHasFatTissue = null)
    {
        if (isActive.HasValue)
        {
            IsActive = isActive.Value;
        }
        if (isHasFatTissue.HasValue)
        {
            IsHasFatTissue = isHasFatTissue.Value;
        }
    }

    /// <summary>
    /// Метод получения объекта свойства с реализацией интерфейса <see cref="IPropertyAction"/>.
    /// Необходим для доступа к действиям свойства из БД.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    internal IPropertyAction GetPropertyAction()
    {
        if (_propertyActionCache.TryGetValue(PropertyUid, out var obj))
        {
            return obj;
        }

        Type? propertyType;
        lock (_lock)
        {
            propertyType = _assembly.GetType(Property.AssemblyName);
        }

        if (propertyType is null)
        {
            throw new InvalidOperationException($"Property [{Property.AssemblyName}] not found!");
        }

        obj = Activator.CreateInstance(propertyType, Property.Uid, Property.Name) as IPropertyAction;
        if (obj is null)
        {
            throw new InvalidOperationException($"Can't create instance of {Property.AssemblyName}!");
        }
        _propertyActionCache.TryAdd(PropertyUid, obj);

        return obj;
    }
}
