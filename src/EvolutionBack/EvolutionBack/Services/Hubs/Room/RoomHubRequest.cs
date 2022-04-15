namespace EvolutionBack.Services.Hubs;

public class RoomHubRequest
{
    public RoomHubRequest(Guid roomUid, RequestType requestType)
    {
        RoomUid = roomUid;
        RequestType = requestType;
    }

    public Guid RoomUid { get; init; }

    public RequestType RequestType { get; init; }
}

public enum RequestType
{
    StartGame = 1,
    StartUserStep = 2,
    EndUserStep = 3,
    StartEvolutionPhase = 4,
    EndEvolutionPhase = 5,
    StartFeedPhase = 6,
    EndFeedPhase = 7,
    StartExtinctionPhase = 8,
    EndExtinctionPhase = 9,
    StartPlantGrowingPhase = 10,
    EndPlantGrowingPhase = 11,
    EndGame = 12,
    PauseGame = 13,
    ResumeGame = 14,
}