using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class CreateAnimalCommand : IRequest
{
    public CreateAnimalCommand(UserCredentials user, Guid cardUid, Guid roomUid)
    {
        User = user;
        CardUid = cardUid;
        RoomUid = roomUid;
    }

    public Guid RoomUid { get; init; }

    public Guid CardUid { get; init; }

    public UserCredentials User { get; init; }
}
