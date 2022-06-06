using AutoMapper;
using Domain.Models;
using Domain.Repo;
using EvolutionBack.Core;
using EvolutionBack.Models;
using Infrastructure.EF;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EvolutionBack.Commands;

public class RoomKickCommandHandler : IRequestHandler<RoomKickCommand, RoomViewModel>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMapper _mapper;

    public RoomKickCommandHandler(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _mapper = mapper;
    }

    public async Task<RoomViewModel> Handle(RoomKickCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        var repo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var user = await userManager.FindByNameAsync(request.User.UserName);

        var obj = repo.Find(request.RoomUid);
        if (obj is null)
        {
            throw new ObjectNotFoundException(request.RoomUid, nameof(Room));
        }

        var requester = obj.FindUserByUid(user.Id) ?? throw new ObjectNotFoundException(user.Id, nameof(User));

        if (!requester.IsHost)
        {
            throw new ValidationException($"User [{user.UserName}] not host!");
        }

        var userForRemove = await userManager.FindByIdAsync(request.UserUid.ToString());

        obj.RemoveUser(userForRemove);

        await dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<RoomViewModel>(obj);
    }
}
