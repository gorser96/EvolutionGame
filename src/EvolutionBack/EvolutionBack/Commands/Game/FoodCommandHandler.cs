using Domain.Repo;
using MediatR;

namespace EvolutionBack.Commands;

public class FoodCommandHandler : IRequestHandler<FoodCommand>
{
    private readonly IAnimalRepo _animalRepo;

    public FoodCommandHandler(IAnimalRepo animalRepo)
    {
        _animalRepo = animalRepo;
    }

    public Task<Unit> Handle(FoodCommand request, CancellationToken cancellationToken)
    {
        var animal = _animalRepo.Find(request.AnimalUid);

        if (animal is null)
        {
            throw new NullReferenceException(nameof(animal));
        }

        animal.Update(foodCurrent: request.Food);

        return Task.FromResult(Unit.Value);
    }
}
