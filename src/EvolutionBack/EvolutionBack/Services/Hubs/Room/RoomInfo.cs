namespace EvolutionBack.Services.Hubs;

public class RoomInfo
{
    public RoomInfo(Guid roomUid, string connectionId, GameService gameService)
    {
        RoomUid = roomUid;
        ConnectionId = connectionId;
        GameService = gameService;
    }

    public Guid RoomUid { get; init; }

    public string ConnectionId { get; init; }

    public GameService GameService { get; set; }
}
