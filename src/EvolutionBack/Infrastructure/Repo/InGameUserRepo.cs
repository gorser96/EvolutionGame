using Domain.Models;
using Domain.Repo;
using Infrastructure.EF;

namespace Infrastructure.Repo;

public class InGameUserRepo : IInGameUserRepo
{
    private readonly EvolutionDbContext _dbContext;

    public InGameUserRepo(EvolutionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public InGameUser Create(Guid userUid, Guid roomUid)
    {
        return _dbContext.InGameUsers.Add(new InGameUser(userUid, roomUid)).Entity;
    }

    public InGameUser? Find(Guid uid)
    {
        return _dbContext.InGameUsers.Find(uid);
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
            _dbContext.InGameUsers.Remove(obj);
            return true;
        }
    }
}
