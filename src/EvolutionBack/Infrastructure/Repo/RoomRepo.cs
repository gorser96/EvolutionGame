using Domain.Models;
using Domain.Repo;
using Domain.Validators;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repo;

public class RoomRepo : IRoomRepo
{
    private readonly EvolutionDbContext _dbContext;
    private readonly IRoomValidator _roomValidator;

    public RoomRepo(EvolutionDbContext dbContext, IRoomValidator roomValidator)
    {
        _dbContext = dbContext;
        _roomValidator = roomValidator;
    }

    public Room Create(Guid uid, string name)
    {
        var obj = _dbContext.Rooms.Add(new Room(uid, name, DateTime.UtcNow)).Entity;
        obj.SetValidator(_roomValidator);
        return obj;
    }

    public Room? Find(Guid uid)
    {
        var obj = _dbContext.Rooms
            .Include(x => x.Additions)
            .Include(x => x.InGameUsers).ThenInclude(x => x.User)
            .Include(x => x.InGameUsers).ThenInclude(x => x.Animals)
            .Include(x => x.InGameUsers).ThenInclude(x => x.Animals).ThenInclude(x => x.Properties)
            .FirstOrDefault(x => x.Uid == uid);
        obj?.SetValidator(_roomValidator);
        return obj;
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
