using EvolutionBack.Core;
using Infrastructure.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace EvolutionTests.TestServices;

internal class WebServiceTest : IDisposable
{
    private readonly ServiceProvider _serviceProvider;
    private readonly Stream _configStream;

    public WebServiceTest()
    {
        var currentId = Task.CurrentId;

        var services = new ServiceCollection();

        var builder = new ConfigurationBuilder();
        _configStream = new MemoryStream();
        using (var memoryStream = new MemoryStream())
        {
            using var writer = new StreamWriter(memoryStream);
            writer.Write(
                "{\n" +
                "\"JWT\": {\n" +
                    "\t\"ValidAudience\": \"http://localhost:4200\",\n" +
                    "\t\"ValidIssuer\": \"http://localhost:61955\",\n" +
                    "\t\"Secret\": \"AyYM010OLl55G6VVVp1OH7Zzyr7gHuK1qvUC5dcGt3SNM\"\n" +
                    "}\n" +
                    "}");
            writer.Flush();

            memoryStream.Position = 0;
            memoryStream.CopyTo(_configStream);
            _configStream.Position = 0;
        }

        builder.AddJsonStream(_configStream);

        var configuration = builder.Build();

        services.AddLogging();
        services.AddScoped<IConfiguration>(_ => configuration);
        services.AddDbContextPool<EvolutionDbContext>(opt =>
        {
            opt.UseInMemoryDatabase(databaseName: $"EvolutionDb{currentId}");

            opt.UseLazyLoadingProxies();
        });
        services.AddCustomAuthentication(configuration);

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

        // validators
        services.AddValidators();

        // AutoMapper
        services.AddMapper();

        _serviceProvider = services.BuildServiceProvider();

        _serviceProvider.UseAnimalProperties();

        //Init();
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
        _configStream.Dispose();
    }
}
