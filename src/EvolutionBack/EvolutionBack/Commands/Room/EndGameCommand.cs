using MediatR;

namespace EvolutionBack.Commands;

public class EndGameCommand : IRequest
{
    public EndGameCommand(Guid roomUid)
    {
        RoomUid = roomUid;
    }

    public Guid RoomUid { get; init; }
}
