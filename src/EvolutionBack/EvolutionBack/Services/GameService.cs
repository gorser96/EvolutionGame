using EvolutionBack.Commands;
using EvolutionBack.Core;
using EvolutionBack.Models;
using EvolutionBack.Queries;
using EvolutionBack.Services.Hubs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EvolutionBack.Services;

public class GameService
{
    private readonly Guid _roomUid;
    private readonly CancellationToken _cancellationToken;

    private readonly IServiceScopeFactory _serviceScopeFactory;

    private bool _isStarted = false;
    private PhaseType _currentPhase;

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

        _currentPhase = PhaseType.Evolution;

        return Task.Run(GameLoop, _cancellationToken);
    }

    internal void StopGame()
    {
        _isStarted = false;
    }

    public void StartEvolutionPhase()
    {
        if (_currentPhase != PhaseType.Extinction)
        {
            throw new ValidationException("Ошибка смены фазы игры. Перед фазой развития должна быть фаза вымирания.");
        }
        _currentPhase = PhaseType.Evolution;
    }

    internal void StartFeedPhase()
    {
        if (_currentPhase != PhaseType.Evolution)
        {
            throw new ValidationException("Ошибка смены фазы игры. Перед фазой питания должна быть фаза развития.");
        }
        _currentPhase = PhaseType.Feed;
    }

    internal void StartExtinctionPhase()
    {
        if (_currentPhase != PhaseType.Feed && _currentPhase != PhaseType.PlantGrowing)
        {
            throw new ValidationException("Ошибка смены фазы игры. Перед фазой вымирания должна быть фаза питания или фаза роста.");
        }
        _currentPhase = PhaseType.Extinction;
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
