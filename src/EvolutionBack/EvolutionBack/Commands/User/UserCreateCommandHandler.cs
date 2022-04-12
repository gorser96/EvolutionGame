using AutoMapper;
using Domain.Repo;
using EvolutionBack.Models;
using Infrastructure.EF;
using MediatR;

namespace EvolutionBack.Commands;

public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, UserViewModel>
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public UserCreateCommandHandler(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }

    public async Task<UserViewModel> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepo>();

        var user = userRepo.Create(request.Login, request.Password, Guid.NewGuid());

        await dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserViewModel>(user);
    }
}
