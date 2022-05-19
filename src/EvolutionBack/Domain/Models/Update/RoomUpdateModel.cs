namespace Domain.Models;

public class RoomUpdateModel
{
    public RoomUpdateModel(
        string? name = null,
        TimeSpan? maxTimeLeft = null,
        ICollection<Addition>? additions = null,
        ICollection<(Guid userUid, int order)>? userOrder = null, bool? isPrivate = null, int? numOfCards = null)
    {
        Name = name;
        MaxTimeLeft = maxTimeLeft;
        Additions = additions;
        UserOrder = userOrder;
        IsPrivate = isPrivate;
        NumOfCards = numOfCards;
    }

    public string? Name { get; private set; }

    public TimeSpan? MaxTimeLeft { get; private set; }

    public ICollection<Addition>? Additions { get; private set; }

    public ICollection<(Guid userUid, int order)>? UserOrder { get; private set; }

    public bool? IsPrivate { get; init; }

    public int? NumOfCards { get; init; }
}
