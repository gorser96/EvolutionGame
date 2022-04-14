using Domain.Models;
using Domain.Repo;
using EvolutionBack.Core;
using Infrastructure.EF;
using MediatR;

namespace EvolutionBack.Commands;

public class PauseGameCommandHandler : IRequestHandler<PauseGameCommand>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PauseGameCommandHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task<Unit> Handle(PauseGameCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        var roomRepo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();

        var room = roomRepo.Find(request.RoomUid);
        if (room is null)
        {
            throw new ObjectNotFoundException(request.RoomUid, nameof(Room));
        }

        room.Pause(request.UserUid);

        db.SaveChanges();

        return Task.FromResult(Unit.Value);
    }
}
