using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class AddPropertyCommand : IRequest
{
    public AddPropertyCommand(Guid roomUid, Guid animalUid, Guid propertyUid, Guid cardUid, UserCredentials user)
    {
        RoomUid = roomUid;
        AnimalUid = animalUid;
        PropertyUid = propertyUid;
        CardUid = cardUid;
        User = user;
    }

    public Guid RoomUid { get; init; }

    public Guid AnimalUid { get; init; }

    public Guid PropertyUid { get; init; }

    public Guid CardUid { get; init; }

    public UserCredentials User { get; init; }
}
