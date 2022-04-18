namespace EvolutionBack.Services.Hubs;

public class GameResponse
{
    public GameResponse(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; init; }
}
