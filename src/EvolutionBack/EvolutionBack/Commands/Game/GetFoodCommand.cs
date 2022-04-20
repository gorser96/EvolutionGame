using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class GetFoodCommand : IRequest
{
    public GetFoodCommand(Guid animalUid, int food, Guid roomUid, UserCredentials user)
    {
        AnimalUid = animalUid;
        Food = food;
        RoomUid = roomUid;
        User = user;
    }

    public Guid RoomUid { get; init; }

    public Guid AnimalUid { get; init; }

    public int Food { get; init; }

    public UserCredentials User { get; init; }
}
