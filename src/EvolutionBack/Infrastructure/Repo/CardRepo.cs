using Domain.Models;
using Domain.Repo;

namespace Infrastructure.Repo;

public class CardRepo : ICardRepo
{
    public Card Create(Guid uid, Guid firstPropertyUid, Guid? secondPropertyUid)
    {
        throw new NotImplementedException();
    }

    public Card Find(Guid uid)
    {
        throw new NotImplementedException();
    }

    public bool Remove(Guid uid)
    {
        throw new NotImplementedException();
    }
}
