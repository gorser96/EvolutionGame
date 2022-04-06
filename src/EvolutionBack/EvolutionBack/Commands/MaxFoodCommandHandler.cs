using Domain.Repo;
using MediatR;

namespace EvolutionBack.Commands;

public class MaxFoodCommandHandler : IRequestHandler<MaxFoodCommand>
{
    private readonly IAnimalRepo _animalRepo;

    public MaxFoodCommandHandler(IAnimalRepo animalRepo)
    {
        _animalRepo = animalRepo;
    }

    public Task<Unit> Handle(MaxFoodCommand request, CancellationToken cancellationToken)
    {
        var animal = _animalRepo.Find(request.AnimalUid);

        if (animal is null)
        {
            throw new NullReferenceException(nameof(animal));
        }

        animal.Update(foodMax: request.MaxFood);

        return Task.FromResult(Unit.Value);
    }
}
