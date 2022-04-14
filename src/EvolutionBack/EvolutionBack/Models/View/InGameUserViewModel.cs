namespace EvolutionBack.Models;

public class InGameUserViewModel
{
    public InGameUserViewModel(UserViewModel user, bool isCurrent, DateTime? startStepTime, int order, bool isHost)
    {
        User = user;
        IsCurrent = isCurrent;
        StartStepTime = startStepTime;
        Order = order;
        IsHost = isHost;
    }

    public UserViewModel User { get; private set; }

    public bool IsCurrent { get; private set; }

    public DateTime? StartStepTime { get; private set; }

    public int Order { get; private set; }

    public bool IsHost { get; private set; }
}
