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

    public void RemoveUser(Guid userUid)
    {
        var user = InGameUsers.FirstOrDefault(x => x.UserUid == userUid) ?? throw new NullReferenceException(nameof(userUid));
        InGameUsers.Remove(user);
    }

    public void AddUser(Guid userUid)
    {
        InGameUsers.Add(new InGameUser(userUid, Uid));
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

    private void UpdateAdditions(ICollection<Addition> additions)
    {
        var source = Additions.ToList();

        foreach (var addition in additions)
        {
            var exist = source.FirstOrDefault(x => x.Uid == addition.Uid);
            if (exist is null)
            {
                Additions.Add(addition);
            }
        }

        var listToRemove = new List<Addition>();
        foreach (var addition in source)
        {
            if (!additions.Any(x => x.Uid == addition.Uid))
            {
                listToRemove.Add(addition);
            }
        }

        foreach (var addition in listToRemove)
        {
            Additions.Remove(addition);
        }
    }

    public void Update(RoomUpdateModel editModel)
    {
        if (editModel.Name is not null)
        {
            SetName(editModel.Name);
        }
        if (editModel.MaxTimeLeft is not null)
        {
            SetMaxTimeLeft(editModel.MaxTimeLeft);
        }
        if (editModel.Additions is not null)
        {
            UpdateAdditions(editModel.Additions);
        }
    }

    public virtual ICollection<InGameUser> InGameUsers { get; private set; }

    public virtual ICollection<Addition> Additions { get; private set; }
}
