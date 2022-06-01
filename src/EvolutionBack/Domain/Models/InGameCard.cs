namespace Domain.Models;

/// <summary>
/// Игровая карточка, которая принадлежит комнате
/// </summary>
public class InGameCard
{
#pragma warning disable CS8618
    public InGameCard(Guid roomUid, Guid cardUid)
    {
        RoomUid = roomUid;
        CardUid = cardUid;
        Order = 0;
    }
#pragma warning restore CS8618

    public Guid RoomUid { get; private set; }

    public virtual Room Room { get; private set; }

    public Guid CardUid { get; private set; }

    public virtual Card Card { get; private set; }

    public Guid? AnimalUid { get; private set; }

    public virtual Animal? Animal { get; private set; }

    /// <summary>
    /// Порядковый номер карточки
    /// </summary>
    public int Order { get; private set; }

    public void Update(int order)
    {
        Order = order;
    }
}
