namespace EvolutionBack.Models;

#pragma warning disable CS8618
public record RoomViewModel
{
    public Guid Uid { get; init; }

    public string Name { get; init; }

    public DateTime CreatedDateTime { get; init; }

    public DateTime? StartDateTime { get; init; }

    public DateTime? FinishedDateTime { get; init; }

    public TimeSpan? MaxTimeLeft { get; init; }

    public int StepNumber { get; init; }

    public bool IsStarted { get; init; }

    public bool IsPaused { get; init; }

    public DateTime? PauseStartedTime { get; init; }

    public int NumOfCards { get; init; }

    public ICollection<InGameUserViewModel> InGameUsers { get; init; }

    public ICollection<AdditionViewModel> Additions { get; init; }
}
#pragma warning restore CS8618

public enum PhaseType
{
    Evolution = 1,
    Feed = 2,
    Extinction = 3,
    PlantGrowing = 4,
}