using EvolutionBack.Commands;
using EvolutionTests.TestServices;
using Infrastructure.EF;
using Infrastructure.EF.Configurations;
using MediatR;
using System;
using System.Linq;
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
        await mediator.Send(command);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Single(db.Users);
        Assert.Equal(command.Login, db.Users.Single().UserName);
    }

    [Fact]
    public async Task Can_login_user()
    {
        await Can_register_user();
        var mediator = _services.Get<IMediator>();

        var command = new UserLoginCommand("test_user", "123test");
        var userView = await mediator.Send(command);

        Assert.Equal(command.Login, userView.UserName);
        Assert.NotEmpty(userView.Token);
    }

    public void Dispose()
    {
        _services.Dispose();
    }
}
