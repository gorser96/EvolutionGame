using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Authentication;

namespace EvolutionBack.Services.Hubs;

[Authorize]
public class GameHub : Hub
{
    private readonly ILogger<GameHub> _logger;
    private readonly HubPublisher _publisher;

    public GameHub(ILogger<GameHub> logger, HubPublisher publisher)
    {
        _logger = logger;
        _publisher = publisher;
    }

    public override Task OnConnectedAsync()
    {
        string name = Context.User?.Identity?.Name ?? throw new AuthenticationException();
        _publisher.AddConnection(name, Context.ConnectionId);

        _logger.LogInformation("New Client connected [{ConnectionId}]", Context.ConnectionId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        string name = Context.User?.Identity?.Name ?? throw new AuthenticationException();
        _publisher.RemoveConnection(name, Context.ConnectionId);

        _logger.LogInformation("Client disconnected [{ConnectionId}]", Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }

    public async Task TestConnectionServer()
    {
        await Clients.Client(Context.ConnectionId).SendAsync("TestConnectionClient");
    }
}
