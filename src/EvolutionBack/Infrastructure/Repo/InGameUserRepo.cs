using Domain.Models;
using Domain.Repo;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

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
        return _dbContext.InGameUsers.Add(new InGameUser(userUid.ToString(), roomUid)).Entity;
    }

    public InGameUser? Find(Guid userUid, Guid roomUid)
    {
        return _dbContext.InGameUsers
            .Include(x => x.User)
            .Include(x => x.Room).ThenInclude(x => x.Additions)
            .Include(x => x.Animals).ThenInclude(x => x.Properties)
            .FirstOrDefault(x => x.UserId == userUid.ToString() && x.RoomUid == roomUid);
    }
}
