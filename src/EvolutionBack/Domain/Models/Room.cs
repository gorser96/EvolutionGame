namespace Domain.Models;

public class Room
{
    public Room(Guid uid, string name, DateTime createdDateTime)
    {
        Uid = uid;
        Name = name;
        CreatedDateTime = createdDateTime;
        InGameUsers = new List<InGameUser>();
        Additions = new List<Addition>();
        StepNumber = 0;
        IsStarted = false;
        IsPaused = false;
        FinishedDateTime = null;
        MaxTimeLeft = null;
    }

    public Guid Uid { get; private set; }

    public string Name { get; private set; }

    public DateTime CreatedDateTime { get; private set; }

    public DateTime? FinishedDateTime { get; private set; }

    public TimeSpan? MaxTimeLeft { get; private set; }

    public int StepNumber { get; private set; }

    public bool IsStarted { get; private set; }

    public bool IsPaused { get; private set; }

    public virtual ICollection<InGameUser> InGameUsers { get; private set; }

    public virtual ICollection<Addition> Additions { get; private set; }
}
