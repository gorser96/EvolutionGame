using AutoMapper;
using Domain.Repo;
using EvolutionBack.Models;
using EvolutionBack.Queries;
using Infrastructure.EF;
using MediatR;
using System.ComponentModel.DataAnnotations;

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
        var userQueries = scope.ServiceProvider.GetRequiredService<UserQueries>();

        var userRoom = userQueries.FindRoomWithUserHost(request.UserUid);
        if (userRoom is not null)
        {
            throw new ValidationException("User already host in other room!");
        }

        var obj = repo.Create(request.Uid, request.Name);
        obj.AddUser(request.UserUid);

        var baseAddition = additionRepo.GetBaseAddition();
        if (baseAddition is not null)
        {
            obj.Init(new[] { baseAddition });
        }

        dbContext.SaveChanges();

        return Task.FromResult(mapper.Map<RoomViewModel>(obj));
    }
}
