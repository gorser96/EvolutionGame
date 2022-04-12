using MediatR;

namespace EvolutionBack.Commands;

public class NextStepCommand : IRequest
{
    public NextStepCommand(Guid roomUid)
    {
        RoomUid = roomUid;
    }

    public Guid RoomUid { get; init; }
}
