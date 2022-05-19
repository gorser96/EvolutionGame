using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class RoomKickCommand : IRequest<RoomViewModel>
{
    public RoomKickCommand(Guid roomUid, Guid userUid, UserCredentials user)
    {
        RoomUid = roomUid;
        UserUid = userUid;
        User = user;
    }

    public Guid RoomUid { get; init; }

    public Guid UserUid { get; init; }

    public UserCredentials User { get; init; }
}
