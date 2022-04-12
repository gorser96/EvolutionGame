using AutoMapper;
using Domain.Repo;
using EvolutionBack.Models;
using Infrastructure.EF;
using MediatR;

namespace EvolutionBack.Commands;

public class RoomCreateCommandHandler : IRequestHandler<RoomCreateCommand, RoomViewModel>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RoomCreateCommandHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task<RoomViewModel> Handle(RoomCreateCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        var repo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();
        var additionRepo = scope.ServiceProvider.GetRequiredService<IAdditionRepo>();
        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

        var obj = repo.Create(request.Uid, request.Name);

        var baseAddition = additionRepo.GetBaseAddition();
        if (baseAddition is not null)
        {
            obj.Update(new Domain.Models.RoomUpdateModel(additions: new[] { baseAddition }));
        }

        dbContext.SaveChanges();

        return Task.FromResult(mapper.Map<RoomViewModel>(obj));
    }
}
