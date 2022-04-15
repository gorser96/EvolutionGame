namespace EvolutionBack.Services.Hubs;

public class MsgFromServer
{
    public MsgFromServer(Guid roomUid, MsgType msgType)
    {
        RoomUid = roomUid;
        MsgType = msgType;
    }

    public Guid RoomUid { get; init; }

    public MsgType MsgType { get; init; }
}

public enum MsgType
{
    NextStep = 1,
}
