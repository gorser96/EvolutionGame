using AutoMapper;
using Domain.Models;
using Domain.Repo;
using EvolutionBack.Core;
using EvolutionBack.Models;
using Infrastructure.EF;
using MediatR;
using Microsoft.AspNetCore.Identity;

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
        var repo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();
        var additionRepo = scope.ServiceProvider.GetRequiredService<IAdditionRepo>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var user = await userManager.FindByNameAsync(request.User.UserName);

        Addition[]? additions = null;
        if (request.EditModel.Additions is not null)
        {
            additions = request.EditModel.Additions
               .Select(x => additionRepo.Find(x) ?? throw new ObjectNotFoundException($"Addition with uid=[{x}] not found!"))
               .ToArray();
        }
        var obj = repo.Find(request.RoomUid);
        if (obj is null)
        {
            throw new ObjectNotFoundException($"Room with uid=[{request.RoomUid}] not found!");
        }

        obj.Update(
            new RoomUpdateModel(
                request.EditModel.Name,
                request.EditModel.MaxTimeLeft,
                additions,
                isPrivate: request.EditModel.IsPrivate,
                numOfCards: request.EditModel.NumOfCards), user.Id);
        await dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<RoomViewModel>(obj);
    }
}
