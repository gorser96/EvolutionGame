using EvolutionBack.Core;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EvolutionTests.TestServices;

internal class WebServiceTest
{
    private readonly ServiceProvider _serviceProvider;

    public WebServiceTest()
    {
        var services = new ServiceCollection();

        // mediator
        services.AddMediatR(Assembly.GetExecutingAssembly());

        // services
        services.AddServices();

        // queries
        services.AddQueries();

        // commands
        services.AddCommandHandlers();

        // repositories
        services.AddRepositories();

        _serviceProvider = services.BuildServiceProvider();
    }

    public TService? Get<TService>() => _serviceProvider.GetService<TService>();
}
