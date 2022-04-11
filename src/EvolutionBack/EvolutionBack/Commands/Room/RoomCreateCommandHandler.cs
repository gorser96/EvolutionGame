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
        var dbContext = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        var repo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();
        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

        var obj = repo.Create(request.Uid, request.Name);
        
        return Task.FromResult(mapper.Map<RoomViewModel>(obj));
    }
}
