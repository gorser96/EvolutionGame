namespace EvolutionBack.Models;

public record ActionResponse
{
    public ActionResponse(AttackResponseType actionType, DefenseData? defenseData = null)
    {
        ActionType = actionType;
        DefenseData = defenseData;
    }

    public AttackResponseType ActionType { get; init; }

    public DefenseData? DefenseData { get; init; }
}

public record DefenseData
{
    public DefenseData(Guid userUid, Guid animalUid, ICollection<Guid> activeProperties)
    {
        UserUid = userUid;
        AnimalUid = animalUid;
        ActiveProperties = activeProperties;
    }

    public Guid UserUid { get; init; }

    public Guid AnimalUid { get; init; }

    public ICollection<Guid> ActiveProperties { get; init; }
}

public enum AttackResponseType
{
    SuccessAttack = 1,
    CantAttack = 2,
    CanDefense = 3,
    UnsuccessAttack = 4,
}
