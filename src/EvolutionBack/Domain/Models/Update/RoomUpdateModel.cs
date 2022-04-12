namespace Domain.Models;

public class RoomUpdateModel
{
    public RoomUpdateModel(
        string? name = null,
        TimeSpan? maxTimeLeft = null,
        ICollection<Addition>? additions = null,
        ICollection<(Guid userUid, int order)>? userOrder = null)
    {
        Name = name;
        MaxTimeLeft = maxTimeLeft;
        Additions = additions;
        UserOrder = userOrder;
    }

    public string? Name { get; private set; }

    public TimeSpan? MaxTimeLeft { get; private set; }

    public ICollection<Addition>? Additions { get; private set; }

    public ICollection<(Guid userUid, int order)>? UserOrder { get; private set; }
}
