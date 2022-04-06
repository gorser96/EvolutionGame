using Domain.Repo;
using EvolutionBack.Commands;
using EvolutionTests.TestServices;
using Infrastructure.EF;
using MediatR;
using System.Threading.Tasks;
using Xunit;

namespace EvolutionTests;

public class UserTests
{
    private readonly WebServiceTest _services;

    public UserTests()
    {
        _services = new();
    }

    [Fact]
    public async Task Can_register_user()
    {
        var command = new UserCreateCommand("test_user", "123test");
        var mediator = _services.Get<IMediator>();
        
        var userView = await mediator.Send(command);
        
        Assert.Equal(command.Login, userView.Login);
        
        var userRepo = _services.Get<IUserRepo>();
        var user = userRepo.Find(userView.Uid);
        Assert.NotNull(user);
        Assert.Equal(command.Login, user?.Login);
    }

    [Fact]
    public async Task Can_login_user()
    {
        await Can_register_user();
        var command = new UserLoginCommand("test_user", "123test");
        var mediator = _services.Get<IMediator>();
        
        var userView = await mediator.Send(command);
        
        Assert.Equal(command.Login, userView.Login);
    }
}
