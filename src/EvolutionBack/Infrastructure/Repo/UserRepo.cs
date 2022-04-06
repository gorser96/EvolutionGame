using Domain.Models;
using Domain.Repo;
using Infrastructure.EF;

namespace Infrastructure.Repo;

public class UserRepo : IUserRepo
{
    private readonly EvolutionDbContext _dbContext;

    public UserRepo(EvolutionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User Create(string login, string password, Guid uid)
    {
        return _dbContext.Users.Add(new User(login, password, uid)).Entity;
    }

    public User? Find(Guid uid)
    {
        return _dbContext.Users.Find(uid);
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
            _dbContext.Users.Remove(obj);
            return true;
        }
    }
}
