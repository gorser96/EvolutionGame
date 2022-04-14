using AutoMapper;
using Domain.Models;
using Domain.Repo;
using Domain.Validators;
using EvolutionBack.Models;
using Infrastructure.EF;
using Infrastructure.Repo;
using Infrastructure.Validators;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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
            services.AddScoped(service);
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
                i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                i.GetGenericTypeDefinition() == typeof(INotificationHandler<>)))
            .ToList();
        foreach (var handler in handlers)
        {
            services.AddScoped(handler);
        }

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRoomRepo, RoomRepo>();
        services.AddScoped<IAdditionRepo, AdditionRepo>();
        services.AddScoped<IPropertyRepo, PropertyRepo>();
        services.AddScoped<IAnimalRepo, AnimalRepo>();
        services.AddScoped<IInGameUserRepo, InGameUserRepo>();

        return services;
    }

    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            cfg.DestinationMemberNamingConvention = new LowerUnderscoreNamingConvention();

            cfg.CreateMap<User, UserViewModel>();
            cfg.CreateMap<Room, RoomViewModel>();
            cfg.CreateMap<Addition, AdditionViewModel>();
            cfg.CreateMap<InGameUser, InGameUserViewModel>();
        }, _assembly);

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IRoomValidator, RoomValidator>();

        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;

                options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<EvolutionDbContext>()
            .AddDefaultTokenProviders();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });

        return services;
    }
}
