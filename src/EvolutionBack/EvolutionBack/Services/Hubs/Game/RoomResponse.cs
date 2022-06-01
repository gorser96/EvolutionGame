namespace EvolutionBack.Services.Hubs;

public class RoomResponse
{
    public RoomResponse(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; init; }
}
