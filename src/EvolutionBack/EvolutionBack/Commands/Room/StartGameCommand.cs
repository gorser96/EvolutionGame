using MediatR;

namespace EvolutionBack.Commands;

public class StartGameCommand : IRequest
{
    public StartGameCommand(Guid roomUid)
    {
        RoomUid = roomUid;
    }

    public Guid RoomUid { get; init; }
}
