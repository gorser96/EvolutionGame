using EvolutionBack.Core;
using Infrastructure.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace EvolutionTests.TestServices;

internal class WebServiceTest
{
    private readonly ServiceProvider _serviceProvider;

    public WebServiceTest()
    {
        var services = new ServiceCollection();

        services.AddDbContextPool<EvolutionDbContext>(opt =>
        {
            opt.UseInMemoryDatabase(databaseName: "EvolutionDb");

            opt.UseLazyLoadingProxies();
        });

        // mediator
        services.AddMediatR(Assembly.GetAssembly(typeof(EvolutionBack.Controllers.UserController)) ?? throw new NullReferenceException());

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

    public TService Get<TService>() where TService : class => _serviceProvider.GetRequiredService<TService>();
}
