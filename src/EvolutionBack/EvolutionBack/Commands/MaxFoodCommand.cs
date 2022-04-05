using MediatR;

namespace EvolutionBack.Commands;

public class MaxFoodCommand : IRequest
{
    public MaxFoodCommand(int maxFood, Guid animalUid)
    {
        MaxFood = maxFood;
        AnimalUid = animalUid;
    }

    public Guid AnimalUid { get; set; }

    public int MaxFood { get; set; }
}
