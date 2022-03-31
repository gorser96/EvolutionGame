namespace Domain.Models;

public class Animal
{
    public Animal(Guid uid, Guid userUid)
    {
        Uid = uid;
        UserUid = userUid;
        Properties = new List<IPropertyAction>();
    }

    public Guid Uid { get; set; }

    public Guid UserUid { get; set; }

    public int Food { get; private set; }

    internal IList<IPropertyAction> Properties { get; set; }
}
