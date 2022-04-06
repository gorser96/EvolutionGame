using Domain.Repo;
using MediatR;

namespace EvolutionBack.Commands
{
    public class AttackCommandHandler : IRequestHandler<AttackCommand>
    {
        private readonly IAnimalRepo _animalRepo;

        public AttackCommandHandler(IAnimalRepo animalRepo)
        {
            _animalRepo = animalRepo;
        }

        public Task<Unit> Handle(AttackCommand request, CancellationToken cancellationToken)
        {
            var attacker = _animalRepo.Find(request.AttackerUid);
            var defensive = _animalRepo.Find(request.DefensiveUid);

            if (attacker is null)
            {
                throw new NullReferenceException(nameof(attacker));
            }

            if (defensive is null)
            {
                throw new NullReferenceException(nameof(defensive));
            }

            defensive.DisableProperties(request.DisabledPropertiesOnAttack);

            attacker.Attack(defensive);

            defensive.EnableProperties(request.DisabledPropertiesOnAttack);

            return Task.FromResult(Unit.Value);
        }
    }
}
