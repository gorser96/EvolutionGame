namespace Domain.Models;

public class AnimalProperty
{
    public AnimalProperty(Guid uid, string name, Guid additionalUid)
    {
        Uid = uid;
        Name = name;
        AdditionalUid = additionalUid;
    }

    public Guid Uid { get; set; }

    public string Name { get; set; }

    public bool IsPair { get; set; }

    public bool IsParasite { get; set; }

    public Guid AdditionalUid { get; set; }
}
