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

    private void SetName(string name)
    {
        if (Name != name)
        {
            Name = name;
        }
    }

    private void SetFinishedDateTime(DateTime? finishedDateTime)
    {
        if (FinishedDateTime != finishedDateTime)
        {
            FinishedDateTime = finishedDateTime;
        }
    }

    private void SetMaxTimeLeft(TimeSpan? maxTimeLeft)
    {
        if (MaxTimeLeft != maxTimeLeft)
        {
            MaxTimeLeft = maxTimeLeft;
        }
    }

    private void SetStepNumber(int stepNumber)
    {
        if (StepNumber != stepNumber)
        {
            StepNumber = stepNumber;
        }
    }

    private void SetIsStarted(bool isStarted)
    {
        if (IsStarted != isStarted)
        {
            IsStarted = isStarted;
        }
    }

    private void SetIsPaused(bool isPaused)
    {
        if (IsPaused != isPaused)
        {
            IsPaused = isPaused;
        }
    }

    private void SetAdditions(ICollection<Addition> additions)
    {
        if (additions.Select(x => x.Uid).Except(Additions.Select(x => x.Uid)).Any() || additions.Count != Additions.Count)
        {
            Additions = additions;
        }
    }

    public void Update(RoomUpdateModel editModel)
    {
        SetName(editModel.Name);
        SetMaxTimeLeft(editModel.MaxTimeLeft);
        SetAdditions(editModel.Additions);
    }

    public virtual ICollection<InGameUser> InGameUsers { get; private set; }

    public virtual ICollection<Addition> Additions { get; private set; }
}
