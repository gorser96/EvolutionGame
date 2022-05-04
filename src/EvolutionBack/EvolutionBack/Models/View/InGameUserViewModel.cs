namespace EvolutionBack.Models;

#pragma warning disable CS8618
public record InGameUserViewModel
{
    public UserViewModel User { get; init; }

    public bool IsCurrent { get; init; }

    public DateTime? StartStepTime { get; init; }

    public int Order { get; init; }

    public bool IsHost { get; init; }
}
#pragma warning restore CS8618
