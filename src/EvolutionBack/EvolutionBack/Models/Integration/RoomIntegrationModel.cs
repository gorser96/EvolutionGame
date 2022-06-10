namespace EvolutionBack.Models;

public record RoomIntegrationModel
{
    public RoomIntegrationModel(Guid roomUid, RoomIntegrationType eventType)
    {
        RoomUid = roomUid;
        EventType = eventType;
    }

    public Guid RoomUid { get; init; }

    public RoomIntegrationType EventType { get; init; }
}

public enum RoomIntegrationType
{
    Created = 1,
    Modified = 2,
    Removed = 3,
    UserJoined = 4,
    UserLeft = 5,
}
