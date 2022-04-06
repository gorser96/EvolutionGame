using Domain.Models;
using Domain.Repo;
using Infrastructure.EF;

namespace Infrastructure.Repo;

public class AnimalRepo : IAnimalRepo
{
    private readonly EvolutionDbContext _dbContext;

    public AnimalRepo(EvolutionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Animal Create(Guid uid, Guid userUid)
    {
        return _dbContext.Animals.Add(new Animal(uid)).Entity;
    }

    public Animal? Find(Guid uid)
    {
        return _dbContext.Animals.Find(uid);
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
            _dbContext.Animals.Remove(obj);
            return true;
        }
    }
}
