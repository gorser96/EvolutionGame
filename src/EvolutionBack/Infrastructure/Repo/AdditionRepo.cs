using Domain.Models;
using Domain.Repo;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repo;

public class AdditionRepo : IAdditionRepo
{
    private readonly EvolutionDbContext _dbContext;

    public AdditionRepo(EvolutionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Addition Create(Guid uid, string name, bool isBase)
    {
        return _dbContext.Additions.Add(new Addition(uid, name, isBase)).Entity;
    }

    public Addition? Find(Guid uid)
    {
        return _dbContext.Additions
            .Include(x => x.Cards)
            .Include(x => x.Cards).ThenInclude(x => x.FirstProperty)
            .Include(x => x.Cards).ThenInclude(x => x.SecondProperty)
            .FirstOrDefault(x => x.Uid == uid);
    }

    public Addition? GetBaseAddition() => _dbContext.Additions
        .Include(x => x.Cards)
        .Include(x => x.Cards).ThenInclude(x => x.FirstProperty)
        .Include(x => x.Cards).ThenInclude(x => x.SecondProperty)
        .SingleOrDefault(x => x.IsBase);

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
