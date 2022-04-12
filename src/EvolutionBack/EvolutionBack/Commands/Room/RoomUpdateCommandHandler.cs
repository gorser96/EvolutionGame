using AutoMapper;
using Domain.Models;
using Domain.Repo;
using EvolutionBack.Models;
using EvolutionBack.Queries;
using Infrastructure.EF;
using MediatR;

namespace EvolutionBack.Commands;

public class RoomUpdateCommandHandler : IRequestHandler<RoomUpdateCommand, RoomViewModel>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMapper _mapper;

    public RoomUpdateCommandHandler(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _mapper = mapper;
    }

    public async Task<RoomViewModel> Handle(RoomUpdateCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        var additionRepo = scope.ServiceProvider.GetRequiredService<IAdditionRepo>();
        var repo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();

        Addition[]? additions = null;
        if (request.EditModel.Additions is not null)
        {
            additions = request.EditModel.Additions
               .Select(x => additionRepo.Find(x) ?? throw new NullReferenceException($"Addition with uid=[{x}] not found!"))
               .ToArray();
        }
        var obj = repo.Find(request.EditModel.Uid);
        if (obj is null)
        {
            throw new NullReferenceException($"Room with uid=[{request.EditModel.Uid}] not found!");
        }

        obj.Update(new RoomUpdateModel(request.EditModel.Name, request.EditModel.MaxTimeLeft, additions));
        await dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<RoomViewModel>(obj);
    }
}
