using Domain.Models;
using Domain.Repo;
using EvolutionBack.Core;
using Infrastructure.EF;
using MediatR;

namespace EvolutionBack.Commands;

public class StartGameCommandHandler : IRequestHandler<StartGameCommand>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public StartGameCommandHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task<Unit> Handle(StartGameCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        var roomRepo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();

        var room = roomRepo.Find(request.RoomUid);
        if (room is null)
        {
            throw new ObjectNotFoundException(request.RoomUid, nameof(Room));
        }

        room.StartGame();

        db.SaveChanges();

        return Task.FromResult(Unit.Value);
    }
}

