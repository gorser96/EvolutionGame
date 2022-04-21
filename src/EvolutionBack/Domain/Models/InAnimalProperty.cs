using System.Reflection;

namespace Domain.Models;

public class InAnimalProperty
{
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

    public bool IsActive { get; private set; }

    public void Update(bool isActive)
    {
        IsActive = isActive;
    }

    internal IPropertyAction GetPropertyAction()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var propertyType = assembly.GetType(Property.AssemblyName);
        if (propertyType is null)
        {
            throw new InvalidOperationException($"Property [{Property.AssemblyName}] not found!");
        }

        if (Activator.CreateInstance(propertyType,
            Property.Uid, Property.Name, Property.IsPair, Property.IsOnEnemy) is not IPropertyAction obj)
        {
            throw new InvalidOperationException($"Can't create instance of {Property.AssemblyName}!");
        }

        obj.SetIsActive(IsActive);

        return obj;
    }
}
