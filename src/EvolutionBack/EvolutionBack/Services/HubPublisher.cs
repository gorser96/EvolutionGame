using EvolutionBack.Services.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace EvolutionBack.Services;

public class HubPublisher
{
    private readonly IHubContext<GameHub> _hub;
    private readonly ConnectionMapping<string> _connections = new();
    private readonly ConcurrentDictionary<string, GameService> _rooms = new();

    public HubPublisher(IHubContext<GameHub> hub)
    {
        _hub = hub;
    }

    public void AddConnection(string name, string connectionId)
    {
        _connections.Add(name, connectionId);
    }

    public void RemoveConnection(string name, string connectionId)
    {
        _connections.Remove(name, connectionId);
    }

    public void UpdateRoom(Guid roomUid)
    {
        var clients = _hub.Clients.Group(roomUid.ToString());
        clients?.SendAsync("UpdatedRoom", roomUid);
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
