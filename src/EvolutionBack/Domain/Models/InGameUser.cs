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
        Order = 0;
    }
#pragma warning restore CS8618

    public Guid UserUid { get; private set; }

    public virtual User User { get; private set; }

    public Guid RoomUid { get; private set; }

    public virtual Room Room { get; private set; }

    public virtual ICollection<Animal> Animals { get; private set; }

    public bool IsCurrent { get; private set; }

    public DateTime? StartStepTime { get; private set; }

    public int Order { get; private set; }

    private void StartStep()
    {
        IsCurrent = true;
        StartStepTime = DateTime.UtcNow;
    }

    private void EndStep()
    {
        IsCurrent = false;
        StartStepTime = null;
    }

    private void SetOrder(int order)
    {
        if (Order != order)
        {
            Order = order;
        }
    }

    public void Update(InGameUserUpdateModel updateModel)
    {
        if (updateModel.IsCurrent.HasValue)
        {
            if (updateModel.IsCurrent.Value)
            {
                StartStep();
            }
            else
            {
                EndStep();
            }
        }

        if (updateModel.Order.HasValue)
        {
            SetOrder(updateModel.Order.Value);
        }
    }
}
