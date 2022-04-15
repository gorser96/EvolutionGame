using AutoMapper;
using EvolutionBack.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Threading.Channels;

namespace EvolutionBack.Services.Hubs;

[Authorize]
public class RoomHub : Hub
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILogger<RoomHub> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private readonly ConcurrentBag<RoomInfo> _rooms;

    public RoomHub(ILogger<RoomHub> logger, IMediator mediator, IMapper mapper, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
        _serviceScopeFactory = serviceScopeFactory;
        _rooms = new ConcurrentBag<RoomInfo>();
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation("New Client connected [{ConnectionId}]", Context.ConnectionId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("Client disconnected [{ConnectionId}]", Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }

    public ChannelReader<RoomHubResponse> RoomCommand(RoomHubRequest request, CancellationToken cancellationToken)
    {
        var channel = Channel.CreateBounded<RoomHubResponse>(new BoundedChannelOptions(int.MaxValue));
        ProcessRequest(channel.Writer, request, cancellationToken);
        return channel.Reader;
    }

    private void ProcessRequest(ChannelWriter<RoomHubResponse> writer, RoomHubRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Request[{Context.ConnectionId}]: {request.RequestType} for room ({request.RoomUid})");

        Exception? localException = null;
        RoomInfo? room = null;
        try
        {
            switch (request.RequestType)
            {
                case RequestType.StartGame:
                    if (_rooms.Select(x => x.RoomUid).Contains(request.RoomUid))
                    {
                        throw new ApplicationException("Room already started");
                    }
                    var roomGameService = new GameService(_serviceScopeFactory, request.RoomUid, cancellationToken);
                    room = new RoomInfo(request.RoomUid, Context.ConnectionId, roomGameService);
                    _rooms.Add(room);
                    roomGameService.StartGame();
                    roomGameService.MessageEvent += OnMessage;
                    break;
                case RequestType.EndGame:
                    room = _rooms.FirstOrDefault(x => x.RoomUid == request.RoomUid);
                    room?.GameService.StopGame();
                    break;
                case RequestType.StartUserStep:
                    break;
                case RequestType.EndUserStep:
                    break;
                case RequestType.StartEvolutionPhase:
                    break;
                case RequestType.EndEvolutionPhase:
                    break;
                case RequestType.StartFeedPhase:
                    break;
                case RequestType.EndFeedPhase:
                    break;
                case RequestType.StartExtinctionPhase:
                    break;
                case RequestType.EndExtinctionPhase:
                    break;
                case RequestType.StartPlantGrowingPhase:
                    break;
                case RequestType.EndPlantGrowingPhase:
                    break;
                case RequestType.PauseGame:
                    break;
                case RequestType.ResumeGame:
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on streaming");
            localException = ex;
        }
        finally
        {
            writer.Complete(localException);
        }
    }

    private void OnMessage(object? sender, MsgFromServer msg)
    {
        var room = _rooms.FirstOrDefault(x => x.RoomUid == msg.RoomUid);
        if (room is null)
        {
            return;
        }
        var client = Clients.Client(room.ConnectionId);
        client?.SendAsync("ReceiveMessage", msg);
    }

    public new void Dispose()
    {
        foreach (var room in _rooms)
        {
            room.GameService.MessageEvent -= OnMessage;
        }
        base.Dispose();
    }
}
