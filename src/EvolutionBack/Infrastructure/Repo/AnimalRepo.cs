using Domain.Models;
using Domain.Repo;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repo;

public class AnimalRepo : IAnimalRepo
{
    private readonly EvolutionDbContext _dbContext;

    public AnimalRepo(EvolutionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Animal Create(Guid uid)
    {
        return _dbContext.Animals.Add(new Animal(uid)).Entity;
    }

    public Animal? Find(Guid uid)
    {
        return _dbContext.Animals
            .Include(x => x.Properties)
            .Include(x => x.InGameUser)
            .FirstOrDefault(x => x.Uid == uid);
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
