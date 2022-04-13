using EvolutionBack.Commands;
using EvolutionBack.Models;
using EvolutionBack.Queries;
using EvolutionTests.TestServices;
using Infrastructure.EF;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EvolutionTests;

public class RoomTests : IDisposable
{
    private readonly WebServiceTest _services;

    public RoomTests()
    {
        _services = new();
    }

    [Fact]
    public async Task Can_create_room()
    {
        var mediator = _services.Get<IMediator>();

        var createCommand = new RoomCreateCommand(Guid.NewGuid(), "test room");
        await mediator.Send(createCommand);

        var roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(createCommand.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Equal("test room", roomViewModel?.Name);
        Assert.Single(roomViewModel?.Additions);
    }

    [Fact]
    public async Task Can_update_room()
    {
        var mediator = _services.Get<IMediator>();

        var createCommand = new RoomCreateCommand(Guid.NewGuid(), "test room");
        await mediator.Send(createCommand);

        var updateCommand = new RoomUpdateCommand(new RoomEditModel(createCommand.Uid, "updated test room", TimeSpan.FromMinutes(2)));
        await mediator.Send(updateCommand);

        var roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(createCommand.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Equal(updateCommand.EditModel.Name, roomViewModel?.Name);
        Assert.Equal(updateCommand.EditModel.MaxTimeLeft, roomViewModel?.MaxTimeLeft);
        Assert.Single(roomViewModel?.Additions);
    }

    [Fact]
    public async Task Can_enter_room()
    {
        var mediator = _services.Get<IMediator>();

        var userCreateCommand = new UserCreateCommand("test_user", "123test");
        await mediator.Send(userCreateCommand);

        var command = new UserLoginCommand("test_user", "123test");
        var userView = await mediator.Send(command);

        var createCommand = new RoomCreateCommand(Guid.NewGuid(), "test room");
        await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(createCommand.Uid, userView.Uid);
        await mediator.Send(enterCommand);

        var roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(createCommand.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Single(roomViewModel?.Additions);
        Assert.Single(roomViewModel?.InGameUsers);
        Assert.Equal(userView.Uid, roomViewModel?.InGameUsers.Single().User.Uid);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Single(db.InGameUsers);
        Assert.Single(db.Rooms);
        Assert.Single(db.Users);
        Assert.Single(db.Additions);
    }

    [Fact]
    public async Task Can_leave_room()
    {
        var mediator = _services.Get<IMediator>();

        var userCreateCommand = new UserCreateCommand("test_user", "123test");
        await mediator.Send(userCreateCommand);

        var command = new UserLoginCommand("test_user", "123test");
        var userView = await mediator.Send(command);

        var createCommand = new RoomCreateCommand(Guid.NewGuid(), "test room");
        await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(createCommand.Uid, userView.Uid);
        await mediator.Send(enterCommand);

        var leaveCommand = new RoomLeaveCommand(createCommand.Uid, userView.Uid);
        await mediator.Send(leaveCommand);

        var roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(createCommand.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Single(roomViewModel?.Additions);
        Assert.Empty(roomViewModel?.InGameUsers);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Empty(db.InGameUsers);
        Assert.Empty(db.Animals);
        // TODO: удалять комнату при отсутствии игроков
        Assert.Single(db.Rooms);
        Assert.Single(db.Users);
        Assert.Single(db.Additions);
    }

    [Fact]
    public async Task Can_remove_room()
    {
        var mediator = _services.Get<IMediator>();

        var userCreateCommand = new UserCreateCommand("test_user", "123test");
        await mediator.Send(userCreateCommand);

        var command = new UserLoginCommand("test_user", "123test");
        var userView = await mediator.Send(command);

        var createCommand = new RoomCreateCommand(Guid.NewGuid(), "test room");
        await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(createCommand.Uid, userView.Uid);
        await mediator.Send(enterCommand);

        var leaveCommand = new RoomLeaveCommand(createCommand.Uid, userView.Uid);
        await mediator.Send(leaveCommand);

        var removeCommand = new RoomRemoveCommand(createCommand.Uid);
        await mediator.Send(removeCommand);

        var roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(createCommand.Uid);

        Assert.Null(roomViewModel);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Empty(db.InGameUsers);
        Assert.Empty(db.Animals);
        Assert.Empty(db.Rooms);
        Assert.Single(db.Users);
        Assert.Single(db.Additions);
    }

    [Fact]
    public async Task Can_start_game()
    {
        var mediator = _services.Get<IMediator>();

        var userCreateCommand = new UserCreateCommand("test_user", "123test");
        await mediator.Send(userCreateCommand);

        var command = new UserLoginCommand("test_user", "123test");
        var userView = await mediator.Send(command);

        var createCommand = new RoomCreateCommand(Guid.NewGuid(), "test room");
        await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(createCommand.Uid, userView.Uid);
        await mediator.Send(enterCommand);

        var startCommand = new StartGameCommand(createCommand.Uid);
        await mediator.Send(startCommand);

        var roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(createCommand.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Single(roomViewModel?.InGameUsers);
        Assert.Single(roomViewModel?.Additions);
        Assert.True(roomViewModel?.IsStarted);
        Assert.False(roomViewModel?.IsPaused);
        Assert.Null(roomViewModel?.FinishedDateTime);
        Assert.Null(roomViewModel?.PauseStartedTime);
        Assert.NotNull(roomViewModel?.StartDateTime);

        Assert.Equal(0, roomViewModel?.InGameUsers.Single().Order);
        Assert.True(roomViewModel?.InGameUsers.Single().IsCurrent);
        Assert.NotNull(roomViewModel?.InGameUsers.Single().StartStepTime);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Single(db.InGameUsers);
        Assert.Empty(db.Animals);
        Assert.Single(db.Rooms);
        Assert.Single(db.Users);
        Assert.Single(db.Additions);
    }

    [Fact]
    public async Task Can_pause_game()
    {
        var mediator = _services.Get<IMediator>();

        var userCreateCommand = new UserCreateCommand("test_user", "123test");
        await mediator.Send(userCreateCommand);

        var command = new UserLoginCommand("test_user", "123test");
        var userView = await mediator.Send(command);

        var createCommand = new RoomCreateCommand(Guid.NewGuid(), "test room");
        await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(createCommand.Uid, userView.Uid);
        await mediator.Send(enterCommand);

        var startCommand = new StartGameCommand(createCommand.Uid);
        await mediator.Send(startCommand);

        var pauseCommand = new PauseGameCommand(createCommand.Uid);
        await mediator.Send(pauseCommand);

        var roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(createCommand.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Single(roomViewModel?.InGameUsers);
        Assert.Single(roomViewModel?.Additions);
        Assert.True(roomViewModel?.IsStarted);
        Assert.True(roomViewModel?.IsPaused);
        Assert.Null(roomViewModel?.FinishedDateTime);
        Assert.NotNull(roomViewModel?.PauseStartedTime);
        Assert.NotNull(roomViewModel?.StartDateTime);

        Assert.Equal(0, roomViewModel?.InGameUsers.Single().Order);
        Assert.True(roomViewModel?.InGameUsers.Single().IsCurrent);
        Assert.NotNull(roomViewModel?.InGameUsers.Single().StartStepTime);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Single(db.InGameUsers);
        Assert.Empty(db.Animals);
        Assert.Single(db.Rooms);
        Assert.Single(db.Users);
        Assert.Single(db.Additions);
    }

    [Fact]
    public async Task Can_resume_game()
    {
        var mediator = _services.Get<IMediator>();

        var userCreateCommand = new UserCreateCommand("test_user", "123test");
        await mediator.Send(userCreateCommand);

        var command = new UserLoginCommand("test_user", "123test");
        var userView = await mediator.Send(command);

        var createCommand = new RoomCreateCommand(Guid.NewGuid(), "test room");
        await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(createCommand.Uid, userView.Uid);
        await mediator.Send(enterCommand);

        var startCommand = new StartGameCommand(createCommand.Uid);
        await mediator.Send(startCommand);

        var pauseCommand = new PauseGameCommand(createCommand.Uid);
        await mediator.Send(pauseCommand);

        var resumeCommand = new ResumeGameCommand(createCommand.Uid);
        await mediator.Send(resumeCommand);

        var roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(createCommand.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Single(roomViewModel?.InGameUsers);
        Assert.Single(roomViewModel?.Additions);
        Assert.True(roomViewModel?.IsStarted);
        Assert.False(roomViewModel?.IsPaused);
        Assert.Null(roomViewModel?.FinishedDateTime);
        Assert.Null(roomViewModel?.PauseStartedTime);
        Assert.NotNull(roomViewModel?.StartDateTime);
        Assert.Equal(0, roomViewModel?.StepNumber);

        Assert.Equal(0, roomViewModel?.InGameUsers.Single().Order);
        Assert.True(roomViewModel?.InGameUsers.Single().IsCurrent);
        Assert.NotNull(roomViewModel?.InGameUsers.Single().StartStepTime);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Single(db.InGameUsers);
        Assert.Empty(db.Animals);
        Assert.Single(db.Rooms);
        Assert.Single(db.Users);
        Assert.Single(db.Additions);
    }

    [Fact]
    public async Task Can_end_game()
    {
        var mediator = _services.Get<IMediator>();

        var userCreateCommand = new UserCreateCommand("test_user", "123test");
        await mediator.Send(userCreateCommand);

        var command = new UserLoginCommand("test_user", "123test");
        var userView = await mediator.Send(command);

        var createCommand = new RoomCreateCommand(Guid.NewGuid(), "test room");
        await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(createCommand.Uid, userView.Uid);
        await mediator.Send(enterCommand);

        var startCommand = new StartGameCommand(createCommand.Uid);
        await mediator.Send(startCommand);

        var pauseCommand = new PauseGameCommand(createCommand.Uid);
        await mediator.Send(pauseCommand);

        var resumeCommand = new ResumeGameCommand(createCommand.Uid);
        await mediator.Send(resumeCommand);

        var nextStepCommand = new NextStepCommand(createCommand.Uid);
        await mediator.Send(nextStepCommand);

        var endCommand = new EndGameCommand(createCommand.Uid);
        await mediator.Send(endCommand);

        var roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(createCommand.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Single(roomViewModel?.InGameUsers);
        Assert.Single(roomViewModel?.Additions);
        Assert.False(roomViewModel?.IsStarted);
        Assert.False(roomViewModel?.IsPaused);
        Assert.NotNull(roomViewModel?.FinishedDateTime);
        Assert.Null(roomViewModel?.PauseStartedTime);
        Assert.NotNull(roomViewModel?.StartDateTime);
        Assert.Equal(1, roomViewModel?.StepNumber);

        Assert.Equal(0, roomViewModel?.InGameUsers.Single().Order);
        Assert.True(roomViewModel?.InGameUsers.Single().IsCurrent);
        Assert.NotNull(roomViewModel?.InGameUsers.Single().StartStepTime);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Single(db.InGameUsers);
        Assert.Empty(db.Animals);
        Assert.Single(db.Rooms);
        Assert.Single(db.Users);
        Assert.Single(db.Additions);
    }

    [Fact]
    public async Task Can_next_step_in_game()
    {
        var mediator = _services.Get<IMediator>();

        var userCreateCommand = new UserCreateCommand("test_user", "123test");
        await mediator.Send(userCreateCommand);

        var command = new UserLoginCommand("test_user", "123test");
        var userView = await mediator.Send(command);

        var createCommand = new RoomCreateCommand(Guid.NewGuid(), "test room");
        await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(createCommand.Uid, userView.Uid);
        await mediator.Send(enterCommand);

        var startCommand = new StartGameCommand(createCommand.Uid);
        await mediator.Send(startCommand);

        var pauseCommand = new PauseGameCommand(createCommand.Uid);
        await mediator.Send(pauseCommand);

        var resumeCommand = new ResumeGameCommand(createCommand.Uid);
        await mediator.Send(resumeCommand);

        var nextStepCommand = new NextStepCommand(createCommand.Uid);
        await mediator.Send(nextStepCommand);

        var roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(createCommand.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Single(roomViewModel?.InGameUsers);
        Assert.Single(roomViewModel?.Additions);
        Assert.True(roomViewModel?.IsStarted);
        Assert.False(roomViewModel?.IsPaused);
        Assert.Null(roomViewModel?.FinishedDateTime);
        Assert.Null(roomViewModel?.PauseStartedTime);
        Assert.NotNull(roomViewModel?.StartDateTime);
        Assert.Equal(1, roomViewModel?.StepNumber);

        Assert.Equal(0, roomViewModel?.InGameUsers.Single().Order);
        Assert.True(roomViewModel?.InGameUsers.Single().IsCurrent);
        Assert.NotNull(roomViewModel?.InGameUsers.Single().StartStepTime);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Single(db.InGameUsers);
        Assert.Empty(db.Animals);
        Assert.Single(db.Rooms);
        Assert.Single(db.Users);
        Assert.Single(db.Additions);
    }

    public void Dispose()
    {
        _services.Dispose();
    }
}
