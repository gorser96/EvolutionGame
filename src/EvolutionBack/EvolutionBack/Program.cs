using EvolutionBack.Core;
using EvolutionBack.Services.Hubs;
using Infrastructure.EF;
using MediatR;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

WebApplicationOptions options = new();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<EvolutionDbContext>((provider, opt) =>
{
    opt.AddInterceptors(provider.GetRequiredService<PreSaveChangesInterceptor>());
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"), sqlOpt => sqlOpt.MigrationsAssembly(nameof(EvolutionBack)));

    opt.UseLazyLoadingProxies();
});

builder.Services.AddCustomAuthentication(builder.Configuration);

// mediator
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// AutoMapper
builder.Services.AddMapper();

// services
builder.Services.AddServices();

// queries
builder.Services.AddQueries();

// commands
builder.Services.AddCommandHandlers();

// repositories
builder.Services.AddRepositories();

// hosted services
builder.Services.AddHostedServices();

// validators
builder.Services.AddValidators();

// SignalR
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders", corsBuilder =>
    {
        corsBuilder
            .WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllHeaders");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<GameHub>("/api/hub", options =>
{
    options.Transports = HttpTransportType.WebSockets;
});

app.Services.UseAnimalProperties();

app.Run();
