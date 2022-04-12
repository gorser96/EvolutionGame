using EvolutionBack.Core;
using Infrastructure.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace EvolutionTests.TestServices;

internal class WebServiceTest : IDisposable
{
    private readonly ServiceProvider _serviceProvider;

    public WebServiceTest()
    {
        var currentId = Task.CurrentId;

        var services = new ServiceCollection();

        services.AddDbContextPool<EvolutionDbContext>(opt =>
        {
            opt.UseInMemoryDatabase(databaseName: $"EvolutionDb{currentId}");

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

        // AutoMapper
        services.AddMapper();

        _serviceProvider = services.BuildServiceProvider();

        Init();
    }

    private void Init()
    {
        using var scope = GetScope();
        using var db = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        db.Additions.Add(new Domain.Models.Addition(Guid.NewGuid(), "Test.BaseAddition", true));
        db.SaveChanges();
    }

    public TService Get<TService>() where TService : class => _serviceProvider.GetRequiredService<TService>();

    public IServiceScope GetScope() => _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

    public void Dispose()
    {
        Get<EvolutionDbContext>().Database.EnsureDeleted();
    }
}
