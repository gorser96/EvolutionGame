using MediatR;

namespace EvolutionBack.Commands;

public class PauseGameCommand : IRequest
{
    public PauseGameCommand(Guid roomUid)
    {
        RoomUid = roomUid;
    }

    public Guid RoomUid { get; init; }
}
