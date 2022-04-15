namespace EvolutionBack.Services.Hubs;

public class RoomHubResponse
{
    public RoomHubResponse(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; init; }
}
