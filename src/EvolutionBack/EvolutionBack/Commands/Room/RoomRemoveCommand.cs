using MediatR;

namespace EvolutionBack.Commands;

public class RoomRemoveCommand : IRequest
{
    public RoomRemoveCommand(Guid roomUid)
    {
        RoomUid = roomUid;
    }

    public Guid RoomUid { get; init; }
}
