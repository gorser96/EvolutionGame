namespace Domain.Models;

public class InGameUserUpdateModel
{
    public InGameUserUpdateModel(int? order = null, bool? isCurrent = null)
    {
        Order = order;
        IsCurrent = isCurrent;
    }

    public bool? IsCurrent { get; init; }

    public int? Order { get; init; }
}
