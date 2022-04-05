namespace Domain.Models;

public class InGameUser
{
    public InGameUser(Guid userUid, Guid roomUid)
    {
        UserUid = userUid;
        RoomUid = roomUid;
        Animals = new List<Animal>();
    }

    public Guid UserUid { get; set; }

    public Guid RoomUid { get; set; }

    public virtual ICollection<Animal> Animals { get; set; }
}
