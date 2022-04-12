using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class RoomLeaveCommand : IRequest<RoomViewModel>
{
    public RoomLeaveCommand(Guid roomUid, Guid userUid)
    {
        RoomUid = roomUid;
        UserUid = userUid;
    }

    public Guid RoomUid { get; init; }

    public Guid UserUid { get; init; }
}
