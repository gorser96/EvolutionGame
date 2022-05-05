using Domain.Models;
using Domain.Repo;
using EvolutionBack.Core;
using EvolutionBack.Models;
using Infrastructure.EF;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EvolutionBack.Commands
{
    public class AttackCommandHandler : IRequestHandler<AttackCommand, ActionResponse>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AttackCommandHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<ActionResponse> Handle(AttackCommand request, CancellationToken cancellationToken)
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

            defensive.DisableProperties(request.DisabledPropertiesOnAttack);

            ActionResponse response;
            var (isSuccessAttack, activeDefenseProperties) = attacker.Attack(defensive);

            if (isSuccessAttack)
            {
                attacker.Feed(2);
                room.RemoveAnimal(user.Id, defensive.Uid);
                response = new(AttackResponseType.SuccessAttack);
            }
            else if (activeDefenseProperties is null)
            {
                defensive.EnableProperties(request.DisabledPropertiesOnAttack);
                response = new(AttackResponseType.CantAttack);
            }
            else
            {
                response = new(AttackResponseType.CanDefense, new(defensive.InGameUserUserUid, defensive.Uid, activeDefenseProperties));
            }

            await db.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
