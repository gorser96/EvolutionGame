namespace Domain.Models;

public class InGameUser
{
#pragma warning disable CS8618
    public InGameUser(Guid userUid, Guid roomUid)
    {
        UserUid = userUid;
        RoomUid = roomUid;
        Animals = new List<Animal>();
        IsCurrent = false;
        StartStepTime = null;
    }
#pragma warning restore CS8618

    public Guid UserUid { get; private set; }

    public virtual User User { get; private set; }

    public Guid RoomUid { get; private set; }

    public virtual Room Room { get; private set; }

    public virtual ICollection<Animal> Animals { get; private set; }

    public bool IsCurrent { get; private set; }

    public DateTime? StartStepTime { get; private set; }
}
