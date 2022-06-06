using EvolutionBack.Services.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace EvolutionBack.Services;

public class HubPublisher
{
    private readonly IHubContext<GameHub> _hub;
    private readonly ILogger<HubPublisher> _logger;
    private readonly ConnectionMapping<string> _connections = new();
    private readonly IDictionary<string, Guid> _groups = new Dictionary<string, Guid>();
    private readonly ConcurrentDictionary<string, GameService> _rooms = new();

    public HubPublisher(IHubContext<GameHub> hub, ILogger<HubPublisher> logger)
    {
        _hub = hub;
        _logger = logger;
    }

    public void AddConnection(string name, string connectionId)
    {
        _connections.Add(name, connectionId);
        if (_groups.TryGetValue(name, out var roomUid))
        {
            JoinToRoom(name, roomUid).Wait();
        }
    }

    public void RemoveConnection(string name, string connectionId)
    {
        _connections.Remove(name, connectionId);
        if (_groups.TryGetValue(name, out var roomUid))
        {
            LeaveRoom(name, roomUid).Wait();
        }
    }

    public async Task UpdatedRoom(Guid roomUid)
    {
        var clients = _hub.Clients.Group(roomUid.ToString());
        if (clients is null)
        {
            return;
        }

        _logger.LogInformation($"Sending UpdatedRoom to group: [name={roomUid}]");
        await clients.SendAsync("UpdatedRoom", new object[] { roomUid });
    }

    public async Task JoinToRoom(string userName, Guid roomUid)
    {
        var connections = _connections.GetConnections(userName);
        if (!connections.Any())
        {
            return;
        }

        var lastConnection = connections.Last();
        _logger.LogInformation($"Connecting user [username={userName}; connectionId={lastConnection}] to group [name={roomUid}]");
        await _hub.Groups.AddToGroupAsync(lastConnection, roomUid.ToString());
        _groups.TryAdd(userName, roomUid);
    }

    public async Task LeaveRoom(string userName, Guid roomUid)
    {
        var connections = _connections.GetConnections(userName);
        if (!connections.Any())
        {
            return;
        }

        var lastConnection = connections.Last();
        _logger.LogInformation($"Disconnecting user [username={userName}; connectionId={lastConnection}] from group [name={roomUid}]");
        await _hub.Groups.RemoveFromGroupAsync(lastConnection, roomUid.ToString());
        _groups.Remove(userName);
    }

    public async Task DeletedRoom(Guid roomUid)
    {
        var clients = _hub.Clients.Group(roomUid.ToString());
        if (clients is null)
        {
            return;
        }

        _logger.LogInformation($"Sending DeletedRoom to group: [name={roomUid}]");
        await clients.SendAsync("DeletedRoom", new object[] { roomUid });
    }
    /*
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
        await _hub.Groups.AddToGroupAsync(Context.ConnectionId, roomUid);
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
        await _hub.Groups.RemoveFromGroupAsync(Context.ConnectionId, roomUid);
    }

    private void OnMessage(object? sender, MsgFromServer msg)
    {
        if (_rooms.TryGetValue(msg.RoomUid.ToString(), out var _))
        {
            var clients = _hub.Clients.Group(msg.RoomUid.ToString());
            clients?.SendAsync("ReceiveMessage", msg);
        }
        else
        {
            return;
        }
    }

    public void Dispose()
    {
        foreach (var room in _rooms)
        {
            room.Value.MessageEvent -= OnMessage;
        }
    }
    */
}
