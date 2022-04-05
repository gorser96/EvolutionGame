using Domain.Repo;
using Infrastructure.Repo;
using MediatR;
using System.Reflection;

namespace EvolutionBack.Core;

public static class ServicesExtensions
{
    private static readonly Assembly _assembly;

    static ServicesExtensions()
    {
        _assembly = Assembly.GetExecutingAssembly();
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        var customServices = _assembly.DefinedTypes
            .Where(x => x.ImplementedInterfaces.Any(i => i == typeof(IService)))
            .ToList();
        foreach (var service in customServices)
        {
            services.AddTransient(service);
        }

        return services;
    }

    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        var queries = _assembly.DefinedTypes
            .Where(x => x.ImplementedInterfaces.Any(i => i == typeof(IQueries)))
            .ToList();
        foreach (var query in queries)
        {
            services.AddTransient(query);
        }

        return services;
    }

    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        var handlers = _assembly.DefinedTypes
            .Where(x => x.ImplementedInterfaces.Where(i => i.IsGenericType).Any(i =>
                i.GetGenericTypeDefinition() == typeof(IRequestHandler<>) ||
                i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
            .ToList();
        foreach (var handler in handlers)
        {
            services.AddTransient(handler);
        }

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepo, UserRepo>();
        services.AddTransient<IRoomRepo, RoomRepo>();
        services.AddTransient<IAdditionRepo, AdditionRepo>();
        services.AddTransient<IPropertyRepo, PropertyRepo>();
        services.AddTransient<IAnimalRepo, AnimalRepo>();
        services.AddTransient<IInGameUserRepo, InGameUserRepo>();

        return services;
    }
}
