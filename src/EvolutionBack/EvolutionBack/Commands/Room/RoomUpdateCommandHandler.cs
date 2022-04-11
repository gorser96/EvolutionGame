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

    public RoomUpdateCommandHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task<RoomViewModel> Handle(RoomUpdateCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        var additionRepo = scope.ServiceProvider.GetRequiredService<IAdditionRepo>();
        var repo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();

        var additions = request.EditModel.Additions
            .Select(x => additionRepo.Find(x) ?? throw new NullReferenceException($"Addition with uid=[{x}] not found!"))
            .ToArray();
        var obj = repo.Find(request.EditModel.Uid);
        if (obj is null)
        {
            throw new NullReferenceException($"Room with uid=[{request.EditModel.Uid}] not found!");
        }

        obj.Update(new RoomUpdateModel(request.EditModel.Name, request.EditModel.MaxTimeLeft, additions));
        dbContext.SaveChanges();

        return Task.FromResult(scope.ServiceProvider.GetRequiredService<RoomQueries>().GetRoomViewModel(request.EditModel.Uid));
    }
}
