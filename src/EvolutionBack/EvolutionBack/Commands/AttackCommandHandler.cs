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

            defensive.DisableProperties(request.DisabledPropertiesOnAttack);

            attacker.Attack(defensive);

            defensive.EnableProperties(request.DisabledPropertiesOnAttack);

            return Task.FromResult(Unit.Value);
        }
    }
}
