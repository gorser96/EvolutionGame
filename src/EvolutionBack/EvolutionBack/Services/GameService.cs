using EvolutionBack.Commands;
using EvolutionBack.Core;
using EvolutionBack.Models;
using EvolutionBack.Queries;
using EvolutionBack.Services.Hubs;
using MediatR;

namespace EvolutionBack.Services;

public class GameService
{
    private readonly Guid _roomUid;
    private readonly CancellationToken _cancellationToken;

    private readonly IServiceScopeFactory _serviceScopeFactory;

    private bool _isStarted = false;

    public GameService(IServiceScopeFactory serviceScopeFactory, Guid roomUid, CancellationToken cancellationToken)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _roomUid = roomUid;
        _cancellationToken = cancellationToken;
    }

    public event EventHandler<MsgFromServer>? MessageEvent;

    public Task StartGame()
    {
        if (_isStarted)
        {
            throw new InvalidOperationException($"Room[{_roomUid}] already started!");
        }

        return Task.Run(GameLoop, _cancellationToken);
    }

    internal void StopGame()
    {
        _isStarted = false;
    }

    private async Task GameLoop()
    {
        _isStarted = true;

        RoomViewModel roomViewModel;
        
        while (_isStarted && !_cancellationToken.IsCancellationRequested)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var roomQueries = scope.ServiceProvider.GetRequiredService<RoomQueries>();
                roomViewModel = roomQueries.GetRoomViewModel(_roomUid) ?? throw new ObjectNotFoundException(_roomUid, nameof(RoomViewModel));
            }
            await CheckUserTimeLeft(roomViewModel);

            await Task.Delay(200);
        }
    }

    private async Task CheckUserTimeLeft(RoomViewModel roomViewModel)
    {
        if (roomViewModel.MaxTimeLeft.HasValue)
        {
            var currentUser = roomViewModel.InGameUsers.First(x => x.IsCurrent);
            if (currentUser.StartStepTime.HasValue && DateTime.UtcNow - currentUser.StartStepTime >= roomViewModel.MaxTimeLeft.Value)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Send(new NextStepCommand(_roomUid));
                }
                MessageEvent?.Invoke(this, new MsgFromServer(_roomUid, MsgType.NextStep));
            }
        }
    }
}
