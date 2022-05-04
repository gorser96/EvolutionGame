using AutoMapper;
using EvolutionBack.Models;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

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

    public RoomViewModel? GetRoomViewModel(Guid uid)
    {
        var obj = _dbContext.Rooms.AsNoTracking()
            .Include(x => x.Additions)
            .Include(x => x.InGameUsers).ThenInclude(x => x.User)
            .Include(x => x.InGameUsers).ThenInclude(x => x.Animals)
            .Include(x => x.Cards).ThenInclude(x => x.Card)
            .FirstOrDefault(x => x.Uid == uid);
        if (obj is null)
        {
            return null;
        }

        return _mapper.Map<RoomViewModel>(obj);
    }

    public ICollection<RoomViewModel> GetRooms()
    {
        var objs = _dbContext.Rooms.AsNoTracking()
            .Include(x => x.Additions)
            .Include(x => x.InGameUsers).ThenInclude(x => x.User)
            .Include(x => x.InGameUsers).ThenInclude(x => x.Animals)
            .Include(x => x.Cards).ThenInclude(x => x.Card)
            .Select(x => _mapper.Map<RoomViewModel>(x))
            .ToArray();
        return objs;
    }
}
