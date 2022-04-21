using EvolutionBack.Core;
using EvolutionBack.Services.Hubs;
using Infrastructure.EF;
using MediatR;
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

builder.Services.AddDbContextPool<EvolutionDbContext>(opt =>
{
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

// validators
builder.Services.AddValidators();

// SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<RoomHub>("/hubs/room");

app.Services.UseAnimalProperties();

app.Run();
