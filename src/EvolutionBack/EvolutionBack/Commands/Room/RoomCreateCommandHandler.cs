using AutoMapper;
using Domain.Models;
using Domain.Repo;
using EvolutionBack.Models;
using EvolutionBack.Queries;
using Infrastructure.EF;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EvolutionBack.Commands;

public class RoomCreateCommandHandler : IRequestHandler<RoomCreateCommand, RoomViewModel>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RoomCreateCommandHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<RoomViewModel> Handle(RoomCreateCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
        var repo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();
        var userQueries = scope.ServiceProvider.GetRequiredService<UserQueries>();
        var additionRepo = scope.ServiceProvider.GetRequiredService<IAdditionRepo>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var user = await userManager.FindByNameAsync(request.User.UserName);

        var userRoom = userQueries.FindRoomWithUserHost(user.Id);
        if (userRoom is not null)
        {
            throw new ValidationException("User already host in other room!");
        }

        var obj = repo.Create(Guid.NewGuid(), request.Name);

        var baseAddition = additionRepo.GetBaseAddition();
        if (baseAddition is not null)
        {
            obj.Init(new[] { baseAddition });
        }
        obj.AddUser(user);

        dbContext.SaveChanges();

        return mapper.Map<RoomViewModel>(obj);
    }
}
