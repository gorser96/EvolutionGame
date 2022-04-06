using Domain.Models;
using Domain.Repo;
using Infrastructure.EF;

namespace Infrastructure.Repo;

public class AdditionRepo : IAdditionRepo
{
    private readonly EvolutionDbContext _dbContext;

    public AdditionRepo(EvolutionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Addition Create(Guid uid, string name)
    {
        return _dbContext.Additions.Add(new Addition(uid, name)).Entity;
    }

    public Addition? Find(Guid uid)
    {
        return _dbContext.Additions.Find(uid);
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
            _dbContext.Additions.Remove(obj);
            return true;
        }
    }
}
