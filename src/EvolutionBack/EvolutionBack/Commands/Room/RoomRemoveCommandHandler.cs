using Domain.Models;
using Domain.Repo;
using EvolutionBack.Core;
using Infrastructure.EF;
using MediatR;

namespace EvolutionBack.Commands;

public class RoomRemoveCommandHandler : IRequestHandler<RoomRemoveCommand>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RoomRemoveCommandHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<Unit> Handle(RoomRemoveCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        var repo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();

        if (!repo.Remove(request.RoomUid))
        {
            throw new ObjectNotFoundException(request.RoomUid, nameof(Room));
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
