using Domain.Models;
using Domain.Repo;
using Infrastructure.EF;

namespace Infrastructure.Repo;

public class CardRepo : ICardRepo
{
    private readonly EvolutionDbContext _dbContext;

    public CardRepo(EvolutionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Card Create(Guid uid, Guid additionUid, Guid firstPropertyUid, Guid? secondPropertyUid)
    {
        return _dbContext.Cards.Add(new Card(uid, additionUid, firstPropertyUid, secondPropertyUid)).Entity;
    }

    public Card? Find(Guid uid)
    {
        return _dbContext.Cards.Find(uid);
    }

    public bool Remove(Guid uid)
    {
        var obj = Find(uid);
        if (obj == null)
        {
            return false;
        }
        else
        {
            _dbContext.Cards.Remove(obj);
            return true;
        }
    }
}
