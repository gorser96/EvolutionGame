using Domain.Models;

namespace Domain.Repo;

public interface IInGameUserRepo
{
    public InGameUser? Find(Guid userUid, Guid roomUid);

    public InGameUser Create(Guid userUid, Guid roomUid);
}
