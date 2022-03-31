namespace Domain.Models;

internal interface IPropertyAction : IProperty
{
    public void Invoke(Animal? self = null, Animal? target = null);
}
