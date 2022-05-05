using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class CreateAnimalCommand : IRequest
{
    public CreateAnimalCommand(UserCredentials user, Guid roomUid)
    {
        User = user;
        RoomUid = roomUid;
    }

    public Guid RoomUid { get; init; }

    public UserCredentials User { get; init; }
}
