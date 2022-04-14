using Domain.Models;
using EvolutionBack.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EvolutionBack.Commands;

public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand>
{
    private readonly UserManager<User> _userManager;

    public UserCreateCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userManager.FindByNameAsync(request.Login);
        if (userExists != null)
        {
            throw new UserAlreadyRegisteredException();
        }
        var user = new User(request.Login, Guid.NewGuid());
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new RegistrationException(result.Errors);
        }

        return Unit.Value;
    }
}
