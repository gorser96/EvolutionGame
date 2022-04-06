using Domain.Models;

namespace Domain.Repo;

public interface IInGameUserRepo
{
    public InGameUser? Find(Guid uid);

    public InGameUser Create(Guid userUid, Guid roomUid);

    public bool Remove(Guid uid);
}
