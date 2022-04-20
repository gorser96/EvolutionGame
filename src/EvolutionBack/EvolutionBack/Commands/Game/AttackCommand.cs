using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class AttackCommand : IRequest
{
    public AttackCommand(Guid attackerUid, Guid defensiveUid,
        IEnumerable<Guid>? disabledPropertiesOnAttack, Guid roomUid, UserCredentials user)
    {
        AttackerUid = attackerUid;
        DefensiveUid = defensiveUid;
        DisabledPropertiesOnAttack = disabledPropertiesOnAttack?.ToList() ?? new List<Guid>();
        RoomUid = roomUid;
        User = user;
    }

    public Guid RoomUid { get; init; }

    public UserCredentials User { get; init; }

    public Guid AttackerUid { get; init; }

    public Guid DefensiveUid { get; init; }

    public IReadOnlyCollection<Guid> DisabledPropertiesOnAttack { get; init; }
}
