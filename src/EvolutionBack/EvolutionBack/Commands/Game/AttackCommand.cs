using MediatR;

namespace EvolutionBack.Commands;

public class AttackCommand : IRequest
{
    public AttackCommand(Guid attackerUid, Guid defensiveUid,
        IEnumerable<Guid> disabledPropertiesOnAttack)
    {
        AttackerUid = attackerUid;
        DefensiveUid = defensiveUid;
        DisabledPropertiesOnAttack = disabledPropertiesOnAttack.ToList();
    }

    public Guid AttackerUid { get; private set; }

    public Guid DefensiveUid { get; private set; }

    public IReadOnlyCollection<Guid> DisabledPropertiesOnAttack { get; private set; }
}
