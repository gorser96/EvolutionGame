namespace Domain.Models;

/// <summary>
/// История игры. Содержит информацию об игроке
/// </summary>
public class GameHistoryUser
{
#pragma warning disable CS8618
    public GameHistoryUser(Guid uid, Guid userUid, Guid gameHistoryUid, int score)
    {
        Uid = uid;
        UserUid = userUid;
        GameHistoryUid = gameHistoryUid;
        Score = score;
    }
#pragma warning restore CS8618

    public Guid Uid { get; init; }

    public Guid UserUid { get; init; }

    public virtual User User { get; private set; }

    public Guid GameHistoryUid { get; init; }

    public virtual GameHistory GameHistory { get; private set; }

    public int Score { get; init; }
}
