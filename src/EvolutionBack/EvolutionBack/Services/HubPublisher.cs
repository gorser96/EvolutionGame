using EvolutionBack.Models;
using EvolutionBack.Services.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace EvolutionBack.Services;

/// <summary>
/// Сервис отправки событий клиентам SignalR
/// </summary>
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

    /// <summary>
    /// Подключение нового клиента, а также обновление комнаты (если пользователь пересоздает подключение, то его не должно выкидывать из комнаты)
    /// </summary>
    /// <param name="name">username пользователя</param>
    /// <param name="connectionId"></param>
    internal void AddConnection(string name, string connectionId)
    {
        _connections.Add(name, connectionId);
        if (_groups.TryGetValue(name, out var roomUid))
        {
            JoinToRoom(name, roomUid).Wait();
        }
    }

    /// <summary>
    /// Отключение клиента и удаление его из комнаты
    /// </summary>
    /// <param name="name">username пользователя</param>
    /// <param name="connectionId"></param>
    internal void RemoveConnection(string name, string connectionId)
    {
        _connections.Remove(name, connectionId);
    }

    /// <summary>
    /// Добавление пользователя в комнату
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="roomUid"></param>
    /// <returns></returns>
    internal async Task JoinToRoom(string userName, Guid roomUid)
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

    /// <summary>
    /// Удаление пользователя из комнаты
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="roomUid"></param>
    /// <returns></returns>
    internal async Task LeaveRoom(string userName, Guid roomUid)
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

    /// <summary>
    /// Отправка интеграционного события события комнаты
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task RoomEvent(RoomIntegrationModel model)
    {
        var clientProxy = model.EventType switch
        {
            RoomIntegrationType.Created or RoomIntegrationType.Removed => _hub.Clients.All,
            RoomIntegrationType.Modified or
            RoomIntegrationType.UserJoined or
            RoomIntegrationType.UserLeft => _hub.Clients.Group(model.RoomUid.ToString()),
            _ => throw new NotSupportedException(nameof(model.EventType)),
        };

        if (clientProxy is null)
        {
            return;
        }

        _logger.LogInformation($"Sending RoomEvent [type={model.EventType}] to group: [name={model.RoomUid}]");
        await clientProxy.SendAsync("RoomEvent", model);
    }
}
