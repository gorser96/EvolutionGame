using Domain.Models;
using Domain.Repo;
using EvolutionBack.Core;
using Infrastructure.EF;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EvolutionBack.Commands;

public class UsePropertyCommandHandler : IRequestHandler<UsePropertyCommand>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UsePropertyCommandHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<Unit> Handle(UsePropertyCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();

        var roomRepo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var user = await userManager.FindByNameAsync(request.User.UserName);

        var room = roomRepo.Find(request.RoomUid);
        if (room is null)
        {
            throw new ObjectNotFoundException(request.RoomUid, nameof(room));
        }

        room.UseAnimalProperty(user.Id, request.SourceAnimalUid, request.PropertyUid, request.TargetAnimalUid);

        await db.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
