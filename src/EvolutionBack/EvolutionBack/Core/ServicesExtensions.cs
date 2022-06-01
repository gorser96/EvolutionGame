using AutoMapper;
using Domain.Models;
using Domain.Repo;
using Domain.Validators;
using EvolutionBack.Models;
using EvolutionBack.Resources;
using EvolutionBack.Services;
using Infrastructure.EF;
using Infrastructure.Repo;
using Infrastructure.Validators;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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

    /// <summary>
    /// Добавление хостинговых сервисов. Данные сервисы создаются в единственном экземпляре,
    /// начинают работу вызовом метода <see cref="IHostedService.StartAsync(CancellationToken)"/>,
    /// заканчивают работу вызовом метода <see cref="IHostedService.StopAsync(CancellationToken)"/> (он вызывается при завершении работы приложения)
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<RoomCleanerServiceHosted>();
        return services;
    }

    /// <summary>
    /// Добавление простых сервисов, реализующих <see cref="IService"/>. Данные сервисы создаются при каждом создании scope
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Добавление простых сервисов запросов, реализующих <see cref="IQueries"/>. Данные сервисы создаются при каждом вызове из DI.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Добавление обработчиков команд библиотеки <see cref="IMediator"/>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Добавление репозиториев объектов.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICardRepo, CardRepo>();
        services.AddScoped<IRoomRepo, RoomRepo>();
        services.AddScoped<IAdditionRepo, AdditionRepo>();
        services.AddScoped<IPropertyRepo, PropertyRepo>();

        return services;
    }

    /// <summary>
    /// Добавление и инициализация <see cref="IMapper"/>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            cfg.DestinationMemberNamingConvention = new LowerUnderscoreNamingConvention();

            cfg.CreateMap<User, UserViewModel>();
            cfg.CreateMap<Room, RoomViewModel>()
                .ForMember(x => x.NumOfCards, x => x.MapFrom(r => r.Cards.Count));
            cfg.CreateMap<Addition, AdditionViewModel>();
            cfg.CreateMap<Card, CardViewModel>();
            cfg.CreateMap<Property, PropertyViewModel>();
            cfg.CreateMap<InGameUser, InGameUserViewModel>();
            cfg.CreateMap<Animal, AnimalViewModel>();
            cfg.CreateMap<InAnimalProperty, InAnimalPropertyViewModel>();
        }, _assembly);

        return services;
    }

    /// <summary>
    /// Добавление валидаторов
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IRoomValidator, RoomValidator>();

        return services;
    }

    /// <summary>
    /// Добавление аутентификации
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentity<User, IdentityRole<Guid>>(options =>
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

    /// <summary>
    /// Инициализация дополнений, карт и свойств животных из json ресурсов
    /// </summary>
    /// <param name="provider"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void UseAnimalProperties(this IServiceProvider provider)
    {
        using var scope = provider.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();

        var propertyActionType = typeof(IPropertyAction);
        var assembly = Assembly.GetAssembly(propertyActionType) ?? throw new InvalidOperationException("Domain assembly not found!");
        var animalProperties = assembly.DefinedTypes
            .Where(x => x.ImplementedInterfaces.Any(i => i == propertyActionType))
            .ToDictionary(x => x.FullName!);

        var cardsJsonStr = File.ReadAllText("Resources/Json/Cards.json");
        var additionsJsonStr = File.ReadAllText("Resources/Json/Additions.json");
        var propertiesJsonStr = File.ReadAllText("Resources/Json/AnimalProperties.json");

        var cardsJson = JsonConvert.DeserializeObject<CardJson>(cardsJsonStr) ?? throw new InvalidOperationException("Can't read cards.json");
        var additionsJson = JsonConvert.DeserializeObject<AdditionJson>(additionsJsonStr) ?? throw new InvalidOperationException("Can't read additions.json");
        var propertiesJson = JsonConvert.DeserializeObject<AnimalPropertyJson>(propertiesJsonStr) ?? throw new InvalidOperationException("Can't read properties.json");

        db.Cards.RemoveRange(db.Cards);
        db.Additions.RemoveRange(db.Additions);
        db.Properties.RemoveRange(db.Properties);

        db.SaveChanges();

        List<Property> dbProperties = new();

        foreach (var property in propertiesJson.Properties)
        {
            if (animalProperties.TryGetValue(property.AssemblyName, out var propertyType))
            {
                if (Activator.CreateInstance(propertyType, Guid.NewGuid(), property.AssemblyName) is Property propertyObj)
                {
                    dbProperties.Add(db.Properties.Add(propertyObj).Entity);
                }
                else
                {
                    throw new InvalidOperationException("Can't create property object!");
                }
            }
            else
            {
                throw new InvalidOperationException("Animal property type not found!");
            }
        }

        db.SaveChanges();

        foreach (var addition in additionsJson.Additions)
        {
            string name = addition.IsBase ? "Базовый набор" : addition.Id.ToString();

            var additionObj = db.Additions.Add(new(Guid.NewGuid(), name, addition.IsBase)).Entity;

            if (addition.IconName is not null)
            {
                var bytes = File.ReadAllBytes(addition.IconName);
                var fileName = Path.GetFileName(addition.IconName);
                additionObj.Update(iconName: fileName, icon: bytes);
            }

            db.SaveChanges();

            foreach (var card in cardsJson.Cards.Where(x => x.AdditionId == addition.Id))
            {
                var firstProperty = dbProperties.FirstOrDefault(x => x.AssemblyName == card.FirstPropertyName);
                if (firstProperty is null)
                {
                    throw new InvalidOperationException($"Property {card.FirstPropertyName} not found!");
                }

                var secondProperty = dbProperties.FirstOrDefault(x => x.AssemblyName == card.SecondPropertyName);
                if (!string.IsNullOrEmpty(card.SecondPropertyName) && secondProperty is null)
                {
                    throw new InvalidOperationException($"Property {card.SecondPropertyName} not found!");
                }

                var cards = Enumerable.Range(1, card.Count)
                    .Select(i => new Card(Guid.NewGuid(), additionObj.Uid, firstProperty.Uid, secondProperty?.Uid))
                    .ToList();
                db.Cards.AddRange(cards);

                db.SaveChanges();
            }
        }
    }
}
