using Domain.Repo;
using EvolutionBack.Commands;
using EvolutionTests.TestServices;
using Infrastructure.EF.Configurations;
using MediatR;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EvolutionTests;

public class UserTests : IDisposable
{
    private readonly WebServiceTest _services;

    public UserTests()
    {
        _services = new();
    }

    [Fact]
    public async Task Can_register_user()
    {
        var mediator = _services.Get<IMediator>();

        var command = new UserCreateCommand("test_user", "123test");
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
        var mediator = _services.Get<IMediator>();

        var command = new UserLoginCommand("test_user", PasswordComputing.GetHash("123test"));
        var userView = await mediator.Send(command);
        
        Assert.Equal(command.Login, userView.Login);
    }

    public void Dispose()
    {
        _services.Dispose();
    }
}
