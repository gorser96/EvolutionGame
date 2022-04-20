namespace Domain.Models;

public class InGameCard
{
#pragma warning disable CS8618
    public InGameCard(Guid roomUid, Guid cardUid, int order)
    {
        RoomUid = roomUid;
        CardUid = cardUid;
        Order = order;
    }
#pragma warning restore CS8618

    public Guid RoomUid { get; private set; }

    public virtual Room Room { get; private set; }

    public Guid CardUid { get; private set; }

    public virtual Card Card { get; private set; }

    public int Order { get; private set; }
}
