using AutoMapper;
using EvolutionBack.Models;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace EvolutionBack.Queries;

public class RoomQueries : IQueries
{
    private readonly EvolutionDbContext _dbContext;
    private readonly IMapper _mapper;

    public RoomQueries(EvolutionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    private IQueryable<Domain.Models.Room> GetIncludedQuery()
    {
        return _dbContext.Rooms.AsNoTracking()
            .Include(x => x.Additions)
            .Include(x => x.Additions).ThenInclude(x => x.Cards)
            .Include(x => x.Additions).ThenInclude(x => x.Cards).ThenInclude(x => x.FirstProperty)
            .Include(x => x.Additions).ThenInclude(x => x.Cards).ThenInclude(x => x.SecondProperty)
            .Include(x => x.InGameUsers).ThenInclude(x => x.User)
            .Include(x => x.InGameUsers).ThenInclude(x => x.Animals).ThenInclude(x => x.Properties).ThenInclude(x => x.Property)
            .Include(x => x.Cards).ThenInclude(x => x.Card);
    }

    public RoomViewModel? GetRoomWithUser(string userName)
    {
        var obj = GetIncludedQuery().FirstOrDefault(x => x.InGameUsers.Any(x => x.User.UserName == userName));
        if (obj is null)
        {
            return null;
        }

        return _mapper.Map<RoomViewModel>(obj);
    }

    public ICollection<InGameUserViewModel> GetUsersFromRoom(Guid uid)
    {
        var obj = GetIncludedQuery()
            .FirstOrDefault(x => x.Uid == uid);
        if (obj is null)
        {
            return Array.Empty<InGameUserViewModel>();
        }
        return obj.InGameUsers.Select(x => _mapper.Map<InGameUserViewModel>(x)).ToArray();
    }

    public RoomViewModel? GetRoomViewModel(Guid uid)
    {
        var obj = GetIncludedQuery()
            .FirstOrDefault(x => x.Uid == uid);
        if (obj is null)
        {
            return null;
        }

        return _mapper.Map<RoomViewModel>(obj);
    }

    public ICollection<RoomViewModel> GetRooms()
    {
        var objs = GetIncludedQuery()
            .Select(x => _mapper.Map<RoomViewModel>(x))
            .ToArray();
        return objs;
    }
}
