using Domain.Validators;

namespace Domain.Models;

public class Room
{
    private IRoomValidator? _roomValidator;

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
        StartDateTime = null;
        PauseStartedTime = null;
    }

    public Guid Uid { get; private set; }

    public string Name { get; private set; }

    public DateTime CreatedDateTime { get; private set; }

    public DateTime? StartDateTime { get; private set; }

    public DateTime? FinishedDateTime { get; private set; }

    public TimeSpan? MaxTimeLeft { get; private set; }

    public int StepNumber { get; private set; }

    public bool IsStarted { get; private set; }

    public bool IsPaused { get; private set; }

    public DateTime? PauseStartedTime { get; private set; }

    private bool IsFinished => FinishedDateTime.HasValue;

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

    private void SetStartDateTime()
    {
        if (StartDateTime is null)
        {
            StartDateTime = DateTime.UtcNow;
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

    public void StartGame(Guid userUid)
    {
        _roomValidator?.CanUserStart(this, userUid);

        if (!IsStarted && !IsFinished)
        {
            SetIsStarted(true);
            SetStartDateTime();

            var firstUser = InGameUsers.First(x => x.Order == 0);
            firstUser.Update(new(isCurrent: true));
        }
    }

    public void NextStep()
    {
        if (IsStarted)
        {
            var currentUser = InGameUsers.Single(x => x.IsCurrent);
            var nextUser = InGameUsers.First(x => x.Order == (currentUser.Order + 1) % InGameUsers.Count);

            currentUser.Update(new(isCurrent: false));
            nextUser.Update(new(isCurrent: true));

            SetStepNumber(StepNumber + 1);
        }
    }

    public void Pause(Guid userUid)
    {
        _roomValidator?.CanUserPause(this, userUid);

        if (IsStarted && !IsPaused)
        {
            SetIsPaused(true);
            PauseStartedTime = DateTime.UtcNow;
        }
    }

    public void Resume(Guid userUid)
    {
        _roomValidator?.CanUserResume(this, userUid);
        if (IsStarted && IsPaused)
        {
            SetIsPaused(false);
            PauseStartedTime = null;
        }
    }

    public void EndGame()
    {
        if (IsStarted)
        {
            SetIsStarted(false);
            SetFinishedDateTime(DateTime.UtcNow);
        }
    }

    public void Init(ICollection<Addition> additions)
    {
        UpdateAdditions(additions);
    }

    public void Update(RoomUpdateModel editModel, Guid userUid)
    {
        _roomValidator?.CanUserUpdate(this, userUid);

        if (!IsStarted && !IsFinished)
        {
            SetMaxTimeLeft(editModel.MaxTimeLeft);

            if (editModel.Name is not null)
            {
                SetName(editModel.Name);
            }
            if (editModel.Additions is not null)
            {
                UpdateAdditions(editModel.Additions);
            }
            if (editModel.UserOrder is not null)
            {
                if (editModel.UserOrder.Count != InGameUsers.Count)
                {
                    throw new ApplicationException($"Count of users is wrong!");
                }

                foreach (var user in InGameUsers)
                {
                    var (_, order) = editModel.UserOrder.Single(x => x.userUid.ToString() == user.UserId);
                    user.Update(new(order: order));
                }
            }
        }
    }

    public void RemoveUser(Guid userUid)
    {
        _roomValidator?.CanUserLeave(this, userUid);

        if (!IsStarted && !IsFinished)
        {
            var user = InGameUsers.FirstOrDefault(x => x.UserId == userUid.ToString()) ?? throw new NullReferenceException(nameof(userUid));
            InGameUsers.Remove(user);
        }
    }

    public void AddUser(Guid userUid)
    {
        _roomValidator?.CanUserEnter(this, userUid);

        if (!IsStarted && !IsFinished)
        {
            // первый игрок, который заходит в комнату становится хостом
            bool isUserHost = !InGameUsers.Any();
            InGameUsers.Add(new InGameUser(userUid.ToString(), Uid, isUserHost));
            InGameUsers.Last().Update(new InGameUserUpdateModel(order: InGameUsers.Count - 1));

        }
    }

    public void SetValidator(IRoomValidator roomValidator)
    {
        _roomValidator = roomValidator;
    }

    public virtual ICollection<InGameUser> InGameUsers { get; private set; }

    public virtual ICollection<Addition> Additions { get; private set; }

    public InGameUser? FindUserByUid(Guid uid)
    {
        return InGameUsers.FirstOrDefault(x => x.UserId == uid.ToString());
    }
}
