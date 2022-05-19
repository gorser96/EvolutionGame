using AutoMapper;
using Domain.Models;
using EvolutionBack.Models;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace EvolutionBack.Queries;

public class AdditionQueries : IQueries
{
    private readonly EvolutionDbContext _dbContext;
    private readonly IMapper _mapper;

    public AdditionQueries(EvolutionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    private IQueryable<Addition> GetIncludeQuery()
    {
        return _dbContext.Additions.AsNoTracking()
            .Include(x => x.Cards)
            .Include(x => x.Cards).ThenInclude(x => x.FirstProperty)
            .Include(x => x.Cards).ThenInclude(x => x.SecondProperty);
    }

    public ICollection<AdditionViewModel> GetAdditions()
    {
        return GetIncludeQuery()
            .Select(x => _mapper.Map<AdditionViewModel>(x))
            .ToArray();
    }

    public AdditionViewModel? GetAddition(Guid uid)
    {
        var obj = GetIncludeQuery().FirstOrDefault(x => x.Uid == uid);
        if (obj is null)
        {
            return null;
        }

        return _mapper.Map<AdditionViewModel>(obj);
    }
}
