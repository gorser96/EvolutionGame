namespace Domain.Models;

public interface IProperty
{
    public Guid Uid { get; set; }

    public string Name { get; set; }

    public string AssemblyName { get; }

    public bool IsPair { get; set; }

    public bool IsOnEnemy { get; set; }

    public Guid AdditionalUid { get; set; }
}
