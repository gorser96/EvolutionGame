using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class RoomEnterCommand : IRequest
{
    public RoomEnterCommand(Guid roomUid, UserCredentials user)
    {
        RoomUid = roomUid;
        User = user;
    }

    public Guid RoomUid { get; init; }

    public UserCredentials User { get; init; }
}
