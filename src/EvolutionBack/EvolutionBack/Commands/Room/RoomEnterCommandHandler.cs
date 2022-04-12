using AutoMapper;
using Domain.Models;
using Domain.Repo;
using EvolutionBack.Core;
using EvolutionBack.Models;
using Infrastructure.EF;
using MediatR;

namespace EvolutionBack.Commands;

public class RoomEnterCommandHandler : IRequestHandler<RoomEnterCommand, RoomViewModel>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMapper _mapper;

    public RoomEnterCommandHandler(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _mapper = mapper;
    }

    public async Task<RoomViewModel> Handle(RoomEnterCommand request, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        var repo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();
        var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepo>();

        var obj = repo.Find(request.RoomUid);
        if (obj is null)
        {
            throw new ObjectNotFoundException(request.RoomUid, nameof(Room));
        }

        var userObj = userRepo.Find(request.UserUid);
        if (userObj is null)
        {
            throw new ObjectNotFoundException(request.UserUid, nameof(User));
        }

        obj.AddUser(request.UserUid);

        await dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<RoomViewModel>(obj);
    }
}
