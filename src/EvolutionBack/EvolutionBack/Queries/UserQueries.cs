using AutoMapper;
using EvolutionBack.Models;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace EvolutionBack.Queries;

public class UserQueries : IQueries
{
    private readonly EvolutionDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserQueries(EvolutionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public RoomViewModel? FindRoomWithUserHost(Guid userUid)
    {
        var room = _dbContext.Rooms.AsNoTracking()
            .Include(x => x.InGameUsers)
            .FirstOrDefault(x => x.InGameUsers.Select(u => u.UserUid).Contains(userUid));
        return _mapper.Map<RoomViewModel>(room);
    }
}
