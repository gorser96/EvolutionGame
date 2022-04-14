using MediatR;

namespace EvolutionBack.Commands;

public class ResumeGameCommand : IRequest
{
    public ResumeGameCommand(Guid roomUid, Guid userUid)
    {
        RoomUid = roomUid;
        UserUid = userUid;
    }

    public Guid RoomUid { get; init; }

    public Guid UserUid { get; init; }
}
