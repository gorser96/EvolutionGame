using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Authentication;

namespace EvolutionBack.Services.Hubs;

[Authorize]
public class GameHub : Hub
{
    private readonly ILogger<GameHub> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private readonly ConnectionMapping<string> _connections = new();
    private readonly ConcurrentDictionary<string, GameService> _rooms;

    public GameHub(ILogger<GameHub> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _rooms = new();
    }

    public override Task OnConnectedAsync()
    {
        string name = Context.User?.Identity?.Name ?? throw new AuthenticationException();
        _connections.Add(name, Context.ConnectionId);

        _logger.LogInformation("New Client connected [{ConnectionId}]", Context.ConnectionId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        string name = Context.User?.Identity?.Name ?? throw new AuthenticationException();
        _connections.Remove(name, Context.ConnectionId);

        _logger.LogInformation("Client disconnected [{ConnectionId}]", Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }

    public async Task TestConnectionServer()
    {
        await Clients.Client(Context.ConnectionId).SendAsync("TestConnectionClient");
    }

    public async Task JoinToRoom(string roomUid)
    {
        if (_rooms.TryGetValue(roomUid, out var gameService))
        {
            gameService.ConnectUser(Context.ConnectionId);
        }
        else
        {
            var roomGameService = new GameService(_serviceScopeFactory, Guid.Parse(roomUid));
            roomGameService.MessageEvent += OnMessage;

            _rooms.TryAdd(roomUid, roomGameService);

            roomGameService.ConnectUser(Context.ConnectionId);
        }
        await Groups.AddToGroupAsync(Context.ConnectionId, roomUid);
    }

    public async Task LeaveRoom(string roomUid)
    {
        if (_rooms.TryGetValue(roomUid, out var gameService))
        {
            gameService.DisconnectUser(Context.ConnectionId);
        }
        else
        {
            throw new BadHttpRequestException($"Room {roomUid} not found!");
        }
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomUid);
    }

    private void OnMessage(object? sender, MsgFromServer msg)
    {
        if (_rooms.TryGetValue(msg.RoomUid.ToString(), out var _))
        {
            var clients = Clients.Group(msg.RoomUid.ToString());
            clients?.SendAsync("ReceiveMessage", msg);
        }
        else
        {
            return;
        }
    }

    public new void Dispose()
    {
        foreach (var room in _rooms)
        {
            room.Value.MessageEvent -= OnMessage;
        }
        base.Dispose();
    }
}
