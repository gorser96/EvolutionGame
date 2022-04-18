using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class UsePropertyCommand : IRequest
{
    public UsePropertyCommand(Guid roomUid, Guid sourceAnimalUid, Guid? targetAnimalUid, Guid propertyUid, UserCredentials user)
    {
        RoomUid = roomUid;
        SourceAnimalUid = sourceAnimalUid;
        TargetAnimalUid = targetAnimalUid;
        PropertyUid = propertyUid;
        User = user;
    }

    public Guid RoomUid { get; init; }

    public Guid SourceAnimalUid { get; init; }

    public Guid? TargetAnimalUid { get; init; }

    public Guid PropertyUid { get; init; }

    public UserCredentials User { get; init; }
}
