using Domain.Models;
using Domain.Repo;
using EvolutionBack.Core;
using EvolutionBack.Models;
using Infrastructure.EF;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EvolutionBack.Commands;

public class DefenseCommandHandler : IRequestHandler<DefenseCommand, ActionResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DefenseCommandHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<ActionResponse> Handle(DefenseCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var roomRepo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var user = await userManager.FindByNameAsync(request.User.UserName);

        var room = roomRepo.Find(request.RoomUid);
        if (room is null)
        {
            throw new ObjectNotFoundException(request.RoomUid, nameof(room));
        }

        var attacker = room.FindAnimal(user.Id, request.AttackerUid);
        var defensive = room.FindAnimal(user.Id, request.DefensiveUid);

        if (attacker is null)
        {
            throw new ObjectNotFoundException(request.AttackerUid, nameof(attacker));
        }

        if (defensive is null)
        {
            throw new ObjectNotFoundException(request.DefensiveUid, nameof(defensive));
        }

        ActionResponse response;
        var defResult = defensive.Defense(attacker, request.DefensePropertyData.PropertyUid, request.DefensePropertyData.TargetUid);
        if (defResult.IsSuccees)
        {
            if (defResult.Data?.TargetAnimal is { } targetAnimal)
            {
                // повторная атака на существо, на которое перенаправили удар
                response = await mediator.Send(
                    new AttackCommand(request.AttackerUid, defResult.Data.TargetAnimal.Uid, null, request.RoomUid, request.User),
                    cancellationToken);
            }
            else if (defResult.Data?.TargetProperty is { } targetProperty)
            {
                defensive.RemoveProperty(targetProperty.Uid);

                response = new(AttackResponseType.UnsuccessAttack);
            }
            else
            {
                response = new(AttackResponseType.UnsuccessAttack);
            }
        }
        else
        {
            response = new(AttackResponseType.SuccessAttack);
        }

        await db.SaveChangesAsync(cancellationToken);

        return response;
    }
}
