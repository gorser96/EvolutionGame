using Domain.Models;
using Domain.Repo;
using EvolutionBack.Core;
using Infrastructure.EF;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EvolutionBack.Commands;

public class StartGameCommandHandler : IRequestHandler<StartGameCommand>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public StartGameCommandHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<Unit> Handle(StartGameCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        var roomRepo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var user = await userManager.FindByNameAsync(request.User.UserName);

        var room = roomRepo.Find(request.RoomUid);
        if (room is null)
        {
            throw new ObjectNotFoundException(request.RoomUid, nameof(Room));
        }

        room.StartGame(user.Id);

        db.SaveChanges();

        return Unit.Value;
    }
}

