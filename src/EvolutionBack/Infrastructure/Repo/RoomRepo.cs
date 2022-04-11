using Domain.Models;
using Domain.Repo;
using Infrastructure.EF;

namespace Infrastructure.Repo;

public class RoomRepo : IRoomRepo
{
    private readonly EvolutionDbContext _dbContext;

    public RoomRepo(EvolutionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Room Create(Guid uid, string name)
    {
        return _dbContext.Rooms.Add(new Room(uid, name, DateTime.UtcNow)).Entity;
    }

    public Room? Find(Guid uid)
    {
        return _dbContext.Rooms.Find(uid);
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
            _dbContext.Rooms.Remove(obj);
            return true;
        }
    }
}
