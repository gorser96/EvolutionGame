namespace Domain.Models;

public class InGameUser
{
    public InGameUser(Guid userUid, Guid roomUid)
    {
        UserUid = userUid;
        RoomUid = roomUid;
        Animals = new List<Animal>();
    }

    public Guid UserUid { get; private set; }

    public virtual User? User { get; private set; }

    public Guid RoomUid { get; private set; }

    public virtual Room? Room { get; private set; }

    public virtual ICollection<Animal> Animals { get; set; }
}
