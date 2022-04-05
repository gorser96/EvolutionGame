using EvolutionBack.Core;
using MediatR;
using System.Reflection;

WebApplicationOptions options = new();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
