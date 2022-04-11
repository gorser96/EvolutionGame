namespace Domain.Models;

public class Card
{
#pragma warning disable CS8618
    public Card(Guid uid, Guid additionUid, Guid firstPropertyUid, Guid? secondPropertyUid)
    {
        Uid = uid;
        AdditionUid = additionUid;
        FirstPropertyUid = firstPropertyUid;
        SecondPropertyUid = secondPropertyUid;
    }
#pragma warning restore CS8618

    public Guid Uid { get; private set; }

    public Guid AdditionUid { get; private set; }

    public virtual Addition Addition { get; private set; }

    public Guid FirstPropertyUid { get; private set; }

    public virtual Property FirstProperty { get; private set; }

    public Guid? SecondPropertyUid { get; private set; }

    public virtual Property? SecondProperty { get; private set; }

    internal void Update(Card obj)
    {
        AdditionUid = obj.AdditionUid;
        FirstPropertyUid = obj.FirstPropertyUid;
        SecondPropertyUid = obj.SecondPropertyUid;
    }
}
