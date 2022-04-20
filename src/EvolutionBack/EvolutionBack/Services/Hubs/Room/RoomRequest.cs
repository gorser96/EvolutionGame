namespace EvolutionBack.Services.Hubs;

public class RoomRequest
{
    public RoomRequest(Guid roomUid, RoomRequestType requestType)
    {
        RoomUid = roomUid;
        RequestType = requestType;
    }

    public Guid RoomUid { get; init; }

    public RoomRequestType RequestType { get; init; }
}

public enum RoomRequestType
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