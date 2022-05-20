using Domain.Validators;

namespace Domain.Models;

/// <summary>
/// Класс игровой комнаты
/// </summary>
public partial class Room
{
    private IRoomValidator? _roomValidator;

    public Room(Guid uid, string name, DateTime createdDateTime)
    {
        Uid = uid;
        Name = name;
        CreatedDateTime = createdDateTime;
        InGameUsers = new List<InGameUser>();
        Additions = new List<Addition>();
        Cards = new List<InGameCard>();
        StepNumber = 0;
        IsStarted = false;
        IsPaused = false;
        FinishedDateTime = null;
        MaxTimeLeft = null;
        StartDateTime = null;
        PauseStartedTime = null;
        IsPrivate = false;
    }

    public Guid Uid { get; init; }

    public string Name { get; private set; }

    public DateTime CreatedDateTime { get; init; }

    public DateTime? StartDateTime { get; private set; }

    public DateTime? FinishedDateTime { get; private set; }

    public TimeSpan? MaxTimeLeft { get; private set; }

    public int StepNumber { get; private set; }

    public bool IsStarted { get; private set; }

    public bool IsPaused { get; private set; }

    public bool IsPrivate { get; private set; }

    public DateTime? PauseStartedTime { get; private set; }

    public virtual ICollection<InGameUser> InGameUsers { get; private set; }

    public virtual ICollection<Addition> Additions { get; private set; }

    /// <summary>
    /// Список оставшихся карт в колоде
    /// </summary>
    public virtual ICollection<InGameCard> Cards { get; private set; }

    private bool IsFinished => FinishedDateTime.HasValue;

    /// <summary>
    /// Добавление валидатора в комнату
    /// </summary>
    /// <param name="roomValidator"></param>
    public void SetValidator(IRoomValidator roomValidator)
    {
        _roomValidator = roomValidator;
    }
}
