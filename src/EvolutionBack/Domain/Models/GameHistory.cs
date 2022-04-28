namespace Domain.Models;

public class GameHistory
{
#pragma warning disable CS8618
    public GameHistory(Guid uid, DateTime createdDateTime, DateTime startDateTime, DateTime finishedDateTime)
    {
        Uid = uid;
        CreatedDateTime = createdDateTime;
        StartDateTime = startDateTime;
        FinishedDateTime = finishedDateTime;
    }
#pragma warning restore CS8618

    public Guid Uid { get; init; }

    public DateTime CreatedDateTime { get; init; }

    public DateTime StartDateTime { get; init; }

    public DateTime FinishedDateTime { get; init; }

    public virtual ICollection<GameHistoryUser> Users { get; private set; }
}
