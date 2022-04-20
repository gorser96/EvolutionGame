using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class AddPairPropertyCommand : IRequest
{
    public AddPairPropertyCommand(
        Guid roomUid, Guid leftAnimalUid, Guid rightAnimalUid, 
        Guid propertyUid, Guid cardUid, UserCredentials user)
    {
        RoomUid = roomUid;
        LeftAnimalUid = leftAnimalUid;
        RightAnimalUid = rightAnimalUid;
        PropertyUid = propertyUid;
        CardUid = cardUid;
        User = user;
    }

    public Guid RoomUid { get; init; }

    public Guid LeftAnimalUid { get; init; }

    public Guid RightAnimalUid { get; init; }

    public Guid PropertyUid { get; init; }

    public Guid CardUid { get; init; }

    public UserCredentials User { get; init; }
}
