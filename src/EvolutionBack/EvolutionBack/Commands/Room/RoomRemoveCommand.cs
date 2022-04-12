using MediatR;

namespace EvolutionBack.Commands;

public class RoomRemoveCommand : IRequest
{
    public RoomRemoveCommand(Guid uid)
    {
        Uid = uid;
    }

    public Guid Uid { get; init; }
}
