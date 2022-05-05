using AutoMapper;
using EvolutionBack.Commands;
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

    public async Task<ChannelReader<RoomResponse>> RoomCommand(RoomRequest request, CancellationToken cancellationToken)
    {
        var channel = Channel.CreateBounded<RoomResponse>(new BoundedChannelOptions(int.MaxValue));
        await ProcessRoomRequest(channel.Writer, request, cancellationToken);
        return channel.Reader;
    }

    public async Task<ChannelReader<GameResponse>> GameCommand(GameRequest request, CancellationToken cancellationToken)
    {
        var channel = Channel.CreateBounded<GameResponse>(new BoundedChannelOptions(int.MaxValue));
        await ProcessGameRequest(channel.Writer, request, cancellationToken);
        return channel.Reader;
    }

    private async Task ProcessRoomRequest(ChannelWriter<RoomResponse> writer, RoomRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Request[{Context.ConnectionId}]: {request.RequestType} for room ({request.RoomUid})");

        Exception? localException = null;
        RoomInfo? room = null;
        string? userName = null;
        try
        {
            switch (request.RequestType)
            {
                case RoomRequestType.StartGame:
                    if (_rooms.Select(x => x.RoomUid).Contains(request.RoomUid))
                    {
                        throw new ApplicationException("Room already started");
                    }
                    var roomGameService = new GameService(_serviceScopeFactory, request.RoomUid, cancellationToken);
                    room = new RoomInfo(request.RoomUid, Context.ConnectionId, roomGameService);
                    _rooms.Add(room);
                    await roomGameService.StartGame();
                    roomGameService.MessageEvent += OnMessage;
                    break;
                case RoomRequestType.EndGame:
                    room = _rooms.FirstOrDefault(x => x.RoomUid == request.RoomUid);
                    room?.GameService.StopGame();
                    break;
                case RoomRequestType.StartUserStep:
                    break;
                case RoomRequestType.EndUserStep:
                    break;
                case RoomRequestType.StartEvolutionPhase:
                    room = _rooms.FirstOrDefault(x => x.RoomUid == request.RoomUid);
                    room?.GameService.StartEvolutionPhase();
                    break;
                case RoomRequestType.EndEvolutionPhase:
                    break;
                case RoomRequestType.StartFeedPhase:
                    room = _rooms.FirstOrDefault(x => x.RoomUid == request.RoomUid);
                    room?.GameService.StartFeedPhase();
                    break;
                case RoomRequestType.EndFeedPhase:
                    break;
                case RoomRequestType.StartExtinctionPhase:
                    room = _rooms.FirstOrDefault(x => x.RoomUid == request.RoomUid);
                    room?.GameService.StartExtinctionPhase();
                    break;
                case RoomRequestType.EndExtinctionPhase:
                    break;
                case RoomRequestType.StartPlantGrowingPhase:
                case RoomRequestType.EndPlantGrowingPhase:
                    throw new NotImplementedException();
                case RoomRequestType.PauseGame:
                    userName = Context.User?.Identity?.Name ?? throw new UnauthorizedAccessException();
                    await _mediator.Send(new PauseGameCommand(request.RoomUid, new UserCredentials(userName)), cancellationToken);
                    break;
                case RoomRequestType.ResumeGame:
                    userName = Context.User?.Identity?.Name ?? throw new UnauthorizedAccessException();
                    await _mediator.Send(new ResumeGameCommand(request.RoomUid, new UserCredentials(userName)), cancellationToken);
                    break;
                default:
                    throw new NotSupportedException($"{request.RequestType} not supported!");
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

    private async Task ProcessGameRequest(ChannelWriter<GameResponse> writer, GameRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Request[{Context.ConnectionId}]: {request.RequestType} for room ({request.RoomUid})");

        Exception? localException = null;
        var room = _rooms.FirstOrDefault(x => x.RoomUid == request.RoomUid);
        var userName = Context.User?.Identity?.Name ?? throw new UnauthorizedAccessException();
        try
        {
            switch (request.RequestType)
            {
                case GameRequestType.CreateAnimal:
                    await _mediator.Send(new CreateAnimalCommand(
                        new UserCredentials(userName),
                        request.RoomUid), cancellationToken);
                    break;
                case GameRequestType.AddProperty:
                    await _mediator.Send(
                        new AddPropertyCommand(
                            request.RoomUid,
                            request.GetFromData<Guid>(nameof(AddPropertyCommand.AnimalUid)),
                            request.GetFromData<Guid>(nameof(AddPropertyCommand.PropertyUid)),
                            request.GetFromData<Guid>(nameof(AddPropertyCommand.CardUid)),
                            new UserCredentials(userName)),
                        cancellationToken);
                    break;
                case GameRequestType.AddPairProperty:
                    await _mediator.Send(
                        new AddPairPropertyCommand(
                            request.RoomUid,
                            request.GetFromData<Guid>(nameof(AddPairPropertyCommand.LeftAnimalUid)),
                            request.GetFromData<Guid>(nameof(AddPairPropertyCommand.RightAnimalUid)),
                            request.GetFromData<Guid>(nameof(AddPairPropertyCommand.PropertyUid)),
                            request.GetFromData<Guid>(nameof(AddPairPropertyCommand.CardUid)),
                            new UserCredentials(userName)),
                        cancellationToken);
                    break;
                case GameRequestType.GetFood:
                    await _mediator.Send(
                        new GetFoodCommand(
                            request.GetFromData<Guid>(nameof(GetFoodCommand.AnimalUid)),
                            request.GetFromData<int>(nameof(GetFoodCommand.Food)),
                            request.RoomUid,
                            new UserCredentials(userName)),
                        cancellationToken);
                    break;
                case GameRequestType.Attack:
                    await _mediator.Send(
                        new AttackCommand(
                            request.GetFromData<Guid>(nameof(AttackCommand.AttackerUid)),
                            request.GetFromData<Guid>(nameof(AttackCommand.DefensiveUid)),
                            request.GetFromData<IEnumerable<Guid>>(nameof(AttackCommand.DisabledPropertiesOnAttack)),
                            request.RoomUid,
                            new UserCredentials(userName)),
                        cancellationToken);
                    break;
                case GameRequestType.UseProperty:
                    await _mediator.Send(
                        new UsePropertyCommand(
                            request.RoomUid,
                            request.GetFromData<Guid>(nameof(UsePropertyCommand.SourceAnimalUid)),
                            request.GetFromData<Guid?>(nameof(UsePropertyCommand.TargetAnimalUid)),
                            request.GetFromData<Guid>(nameof(UsePropertyCommand.PropertyUid)),
                            new UserCredentials(userName)),
                        cancellationToken);
                    break;
                default:
                    throw new NotSupportedException($"{request.RequestType} not supported!");
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
