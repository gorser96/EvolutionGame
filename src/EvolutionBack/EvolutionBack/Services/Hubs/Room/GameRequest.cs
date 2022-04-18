namespace EvolutionBack.Services.Hubs;

public class GameRequest
{
    public GameRequest(Guid roomUid, GameRequestType requestType)
    {
        RoomUid = roomUid;
        RequestType = requestType;
    }

    public Guid RoomUid { get; init; }

    public GameRequestType RequestType { get; init; }
}

public enum GameRequestType
{
    CreateAnimal = 1,
    AddProperty = 2,
    AddPairProperty = 3,
    GetFood = 4,
    Attack = 5,
    UseProperty = 6,
}