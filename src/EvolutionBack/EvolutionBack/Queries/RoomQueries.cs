using AutoMapper;
using EvolutionBack.Models;
using Infrastructure.EF;

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

    public RoomViewModel GetRoomViewModel(Guid uid)
    {
        var obj = _dbContext.Rooms.Find(uid);
        if (obj is null)
        {
            throw new NullReferenceException($"Object room.Uid=[{uid}] not found!");
        }

        return _mapper.Map<RoomViewModel>(obj);
    }
}
