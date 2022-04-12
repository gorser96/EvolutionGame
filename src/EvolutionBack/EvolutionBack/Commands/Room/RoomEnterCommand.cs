using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class RoomEnterCommand : IRequest<RoomViewModel>
{
    public RoomEnterCommand(Guid roomUid, Guid userUid)
    {
        RoomUid = roomUid;
        UserUid = userUid;
    }

    public Guid RoomUid { get; init; }

    public Guid UserUid { get; init; }
}
