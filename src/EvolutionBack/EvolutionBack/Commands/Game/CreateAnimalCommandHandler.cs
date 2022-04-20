using Domain.Repo;
using Infrastructure.EF;
using MediatR;

namespace EvolutionBack.Commands;

public class CreateAnimalCommandHandler : IRequestHandler<CreateAnimalCommand>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CreateAnimalCommandHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task<Unit> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        var animalRepo = scope.ServiceProvider.GetRequiredService<IAnimalRepo>();

        throw new NotImplementedException();
    }
}
