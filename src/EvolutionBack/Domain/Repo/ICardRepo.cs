using Domain.Models;

namespace Domain.Repo;

public interface ICardRepo
{
    public Card? Find(Guid uid);

    public Card Create(Guid uid, Guid additionUid, Guid firstPropertyUid, Guid? secondPropertyUid);

    public bool Remove(Guid uid);
}
