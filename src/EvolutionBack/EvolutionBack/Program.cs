using EvolutionBack.Core;
using Infrastructure.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

WebApplicationOptions options = new();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<EvolutionDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"), sqlOpt => sqlOpt.MigrationsAssembly(nameof(EvolutionBack)));

    opt.UseLazyLoadingProxies();
});

// mediator
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// services
builder.Services.AddServices();

// queries
builder.Services.AddQueries();

// commands
builder.Services.AddCommandHandlers();

// repositories
builder.Services.AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
