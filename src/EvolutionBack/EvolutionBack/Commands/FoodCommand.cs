using MediatR;

namespace EvolutionBack.Commands;

public class FoodCommand : IRequest
{
    public FoodCommand(Guid animalUid, int food)
    {
        AnimalUid = animalUid;
        Food = food;
    }

    public Guid AnimalUid { get; set; }

    public int Food { get; set; }
}
