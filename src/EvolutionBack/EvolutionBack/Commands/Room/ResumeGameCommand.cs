using MediatR;

namespace EvolutionBack.Commands;

public class ResumeGameCommand : IRequest
{
    public ResumeGameCommand(Guid roomUid)
    {
        RoomUid = roomUid;
    }

    public Guid RoomUid { get; init; }
}
