using Domain.Models;
using Domain.Repo;
using EvolutionBack.Core;
using Infrastructure.EF;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EvolutionBack.Commands;

public class AddPairPropertyCommandHandler : IRequestHandler<AddPairPropertyCommand>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AddPairPropertyCommandHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<Unit> Handle(AddPairPropertyCommand request, CancellationToken cancellationToken)
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

        room.AddAnimalProperty(user.Id, request.LeftAnimalUid, request.CardUid, request.PropertyUid);
        room.AddAnimalProperty(user.Id, request.RightAnimalUid, request.CardUid, request.PropertyUid);

        await db.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
