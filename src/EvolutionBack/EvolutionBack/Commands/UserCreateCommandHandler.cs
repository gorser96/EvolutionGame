using Domain.Repo;
using EvolutionBack.Models;
using Infrastructure.EF;
using MediatR;

namespace EvolutionBack.Commands;

public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, UserViewModel>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public UserCreateCommandHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task<UserViewModel> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepo>();

        var user = userRepo.Create(request.Login, request.Password, Guid.NewGuid());

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UserViewModel(user.Login, user.Uid);
    }
}
