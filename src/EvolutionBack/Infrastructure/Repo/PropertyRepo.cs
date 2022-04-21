using Domain.Models;
using Domain.Repo;
using Infrastructure.EF;

namespace Infrastructure.Repo;

public class PropertyRepo : IPropertyRepo
{
    private readonly EvolutionDbContext _dbContext;

    public PropertyRepo(EvolutionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Property Create(Guid uid, string name, bool isPair, bool isOnEnemy, int? feedIncreasing, string assemblyName)
    {
        return _dbContext.Properties.Add(new Property(uid, name, isPair, isOnEnemy, feedIncreasing, assemblyName)).Entity;
    }

    public Property? Find(Guid uid)
    {
        return _dbContext.Properties.Find(uid);
    }

    public Property? FindByAssemblyName(string name)
    {
        return _dbContext.Properties.FirstOrDefault(x => x.AssemblyName == name);
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
            _dbContext.Properties.Remove(obj);
            return true;
        }
    }
}
