namespace EvolutionBack.Models;

public class RoomViewModel
{
    public RoomViewModel(
        Guid uid, string name, 
        DateTime createdDateTime, DateTime? finishedDateTime, TimeSpan? maxTimeLeft, 
        int stepNumber, bool isStarted, bool isPaused, 
        IEnumerable<InGameUserViewModel> inGameUsers, IEnumerable<AdditionViewModel> additions)
    {
        Uid = uid;
        Name = name;
        CreatedDateTime = createdDateTime;
        FinishedDateTime = finishedDateTime;
        MaxTimeLeft = maxTimeLeft;
        StepNumber = stepNumber;
        IsStarted = isStarted;
        IsPaused = isPaused;
        InGameUsers = inGameUsers.ToList();
        Additions = additions.ToList();
    }

    public Guid Uid { get; private set; }

    public string Name { get; private set; }

    public DateTime CreatedDateTime { get; private set; }

    public DateTime? FinishedDateTime { get; private set; }

    public TimeSpan? MaxTimeLeft { get; private set; }

    public int StepNumber { get; private set; }

    public bool IsStarted { get; private set; }

    public bool IsPaused { get; private set; }

    public IReadOnlyCollection<InGameUserViewModel> InGameUsers { get; private set; }

    public IReadOnlyCollection<AdditionViewModel> Additions { get; private set; }
}
