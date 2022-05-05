using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class DefenseCommand : IRequest<ActionResponse>
{
    public DefenseCommand(Guid roomUid, UserCredentials user, Guid attackerUid, Guid defensiveUid, DefenseProperty defensePropertyData)
    {
        RoomUid = roomUid;
        User = user;
        AttackerUid = attackerUid;
        DefensiveUid = defensiveUid;
        DefensePropertyData = defensePropertyData;
    }

    public Guid RoomUid { get; init; }

    public UserCredentials User { get; init; }

    public Guid AttackerUid { get; init; }

    public Guid DefensiveUid { get; init; }

    public DefenseProperty DefensePropertyData { get; init; }
}

public record DefenseProperty
{
    public DefenseProperty(Guid propertyUid, Guid? targetUid = null)
    {
        PropertyUid = propertyUid;
        TargetUid = targetUid;
    }

    public Guid PropertyUid { get; init; }

    public Guid? TargetUid { get; init; }
}
