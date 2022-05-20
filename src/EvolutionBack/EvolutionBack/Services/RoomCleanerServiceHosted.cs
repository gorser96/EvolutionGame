using Domain.Repo;
using EvolutionBack.Queries;
using Infrastructure.EF;

namespace EvolutionBack.Services;

/// <summary>
/// Сервис, который удаляем старые/неактивные комнаты
/// </summary>
public class RoomCleanerServiceHosted : IHostedService, IDisposable
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<RoomCleanerServiceHosted> _logger;

    private Timer? _timer;

    public RoomCleanerServiceHosted(IServiceScopeFactory serviceScopeFactory, ILogger<RoomCleanerServiceHosted> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(OnTick, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        _logger.LogInformation("Service started");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        _logger.LogInformation("Service stopped");
        return Task.CompletedTask;
    }

    private void OnTick(object? source)
    {
        _logger.LogInformation("Clean started");
        using var scope = _serviceScopeFactory.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<EvolutionDbContext>();
        var roomQueries = scope.ServiceProvider.GetRequiredService<RoomQueries>();
        var roomRepo = scope.ServiceProvider.GetRequiredService<IRoomRepo>();

        var rooms = roomQueries.GetRooms();

        var nonActiveTime = TimeSpan.FromHours(3);

        foreach (var room in rooms)
        {
            if ((room.StartDateTime.HasValue && (DateTime.UtcNow - room.StartDateTime > nonActiveTime)) ||
                (room.InGameUsers.Count <= 1 && DateTime.UtcNow - room.CreatedDateTime > nonActiveTime) ||
                (room.FinishedDateTime.HasValue && DateTime.UtcNow - room.FinishedDateTime > TimeSpan.FromMinutes(30)))
            {
                _logger.LogInformation("Removing room: uid={}; name={}", room.Uid, room.Name);
                roomRepo.Remove(room.Uid);
            }
        }

        db.SaveChanges();
    }
}
