using Domain.Models;
using Domain.Repo;
using EvolutionBack.Core;
using Infrastructure.EF;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EvolutionBack.Commands
{
    public class AttackCommandHandler : IRequestHandler<AttackCommand>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AttackCommandHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<Unit> Handle(AttackCommand request, CancellationToken cancellationToken)
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

            if (attacker.Attack(defensive))
            {
                attacker.Feed(2);
                room.RemoveAnimal(user.Id, defensive.Uid);
            }
            else
            {
                defensive.EnableProperties(request.DisabledPropertiesOnAttack);
            }

            await db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
