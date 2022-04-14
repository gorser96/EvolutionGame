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

        var userCreateCommand = new UserCreateCommand("test_user", "123test");
        await mediator.Send(userCreateCommand);

        var command = new UserLoginCommand("test_user", "123test");
        var userView = await mediator.Send(command);

        var createCommand = new RoomCreateCommand("test room", new(userView.UserName));
        var roomViewModel = await mediator.Send(createCommand);

        Assert.NotNull(roomViewModel);
        Assert.Equal("test room", roomViewModel?.Name);
        Assert.Single(roomViewModel?.Additions);
    }

    [Fact]
    public async Task Can_update_room()
    {
        var mediator = _services.Get<IMediator>();

        var userCreateCommand = new UserCreateCommand("test_user", "123test");
        await mediator.Send(userCreateCommand);

        var command = new UserLoginCommand("test_user", "123test");
        var userView = await mediator.Send(command);

        var createCommand = new RoomCreateCommand("test room", new(userView.UserName));
        var roomViewModel = await mediator.Send(createCommand);

        var updateCommand = new RoomUpdateCommand(new RoomEditModel("updated test room", TimeSpan.FromMinutes(2)), new(userView.UserName), roomViewModel.Uid);
        await mediator.Send(updateCommand);

        roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(roomViewModel.Uid);

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

        var userCreateCommand2 = new UserCreateCommand("test_user2", "123test");
        await mediator.Send(userCreateCommand2);

        var command2 = new UserLoginCommand("test_user2", "123test");
        var userView2 = await mediator.Send(command2);

        var createCommand = new RoomCreateCommand("test room", new(userView.UserName));
        var roomViewModel = await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(roomViewModel.Uid, new(userView2.UserName));
        await mediator.Send(enterCommand);

        roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(roomViewModel.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Single(roomViewModel?.Additions);
        Assert.Equal(2, roomViewModel?.InGameUsers.Count);
        Assert.Equal(userView.UserName, roomViewModel?.InGameUsers.First(x => x.IsHost).User.UserName);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Equal(2, db.InGameUsers.Count());
        Assert.Single(db.Rooms);
        Assert.Equal(2, db.Users.Count());
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

        var userCreateCommand2 = new UserCreateCommand("test_user2", "123test");
        await mediator.Send(userCreateCommand2);

        var command2 = new UserLoginCommand("test_user2", "123test");
        var userView2 = await mediator.Send(command2);

        var createCommand = new RoomCreateCommand("test room", new(userView.UserName));
        var roomViewModel = await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(roomViewModel.Uid, new(userView2.UserName));
        await mediator.Send(enterCommand);

        var leaveCommand = new RoomLeaveCommand(roomViewModel.Uid, new(userView2.UserName));
        await mediator.Send(leaveCommand);

        roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(roomViewModel.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Single(roomViewModel?.Additions);
        Assert.Single(roomViewModel?.InGameUsers);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Single(db.InGameUsers);
        Assert.Empty(db.Animals);
        // TODO: удалять комнату при отсутствии игроков
        Assert.Single(db.Rooms);
        Assert.Equal(2, db.Users.Count());
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

        var userCreateCommand2 = new UserCreateCommand("test_user2", "123test");
        await mediator.Send(userCreateCommand2);

        var command2 = new UserLoginCommand("test_user2", "123test");
        var userView2 = await mediator.Send(command2);

        var createCommand = new RoomCreateCommand("test room", new(userView.UserName));
        var roomViewModel = await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(roomViewModel.Uid, new(userView2.UserName));
        await mediator.Send(enterCommand);

        var leaveCommand = new RoomLeaveCommand(roomViewModel.Uid, new(userView.UserName));
        await mediator.Send(leaveCommand);

        var removeCommand = new RoomRemoveCommand(roomViewModel.Uid);
        await mediator.Send(removeCommand);

        roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(roomViewModel.Uid);

        Assert.Null(roomViewModel);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Empty(db.InGameUsers);
        Assert.Empty(db.Animals);
        Assert.Empty(db.Rooms);
        Assert.Equal(2, db.Users.Count());
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

        var userCreateCommand2 = new UserCreateCommand("test_user2", "123test");
        await mediator.Send(userCreateCommand2);

        var command2 = new UserLoginCommand("test_user2", "123test");
        var userView2 = await mediator.Send(command2);

        var createCommand = new RoomCreateCommand("test room", new(userView.UserName));
        var roomViewModel = await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(roomViewModel.Uid, new(userView2.UserName));
        await mediator.Send(enterCommand);

        var startCommand = new StartGameCommand(roomViewModel.Uid, new(userView.UserName));
        await mediator.Send(startCommand);

        roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(roomViewModel.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Equal(2, roomViewModel?.InGameUsers.Count);
        Assert.Single(roomViewModel?.Additions);
        Assert.True(roomViewModel?.IsStarted);
        Assert.False(roomViewModel?.IsPaused);
        Assert.Null(roomViewModel?.FinishedDateTime);
        Assert.Null(roomViewModel?.PauseStartedTime);
        Assert.NotNull(roomViewModel?.StartDateTime);

        Assert.Equal(0, roomViewModel?.InGameUsers.First(x => x.IsHost).Order);
        Assert.True(roomViewModel?.InGameUsers.First(x => x.IsHost).IsCurrent);
        Assert.NotNull(roomViewModel?.InGameUsers.First(x => x.IsHost).StartStepTime);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Equal(2, db.InGameUsers.Count());
        Assert.Empty(db.Animals);
        Assert.Single(db.Rooms);
        Assert.Equal(2, db.Users.Count());
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

        var userCreateCommand2 = new UserCreateCommand("test_user2", "123test");
        await mediator.Send(userCreateCommand2);

        var command2 = new UserLoginCommand("test_user2", "123test");
        var userView2 = await mediator.Send(command2);

        var createCommand = new RoomCreateCommand("test room", new(userView.UserName));
        var roomViewModel = await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(roomViewModel.Uid, new(userView2.UserName));
        await mediator.Send(enterCommand);

        var startCommand = new StartGameCommand(roomViewModel.Uid, new(userView.UserName));
        await mediator.Send(startCommand);

        var pauseCommand = new PauseGameCommand(roomViewModel.Uid, new(userView.UserName));
        await mediator.Send(pauseCommand);

        roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(roomViewModel.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Equal(2, roomViewModel?.InGameUsers.Count);
        Assert.Single(roomViewModel?.Additions);
        Assert.True(roomViewModel?.IsStarted);
        Assert.True(roomViewModel?.IsPaused);
        Assert.Null(roomViewModel?.FinishedDateTime);
        Assert.NotNull(roomViewModel?.PauseStartedTime);
        Assert.NotNull(roomViewModel?.StartDateTime);

        Assert.Equal(0, roomViewModel?.InGameUsers.First(x => x.IsHost).Order);
        Assert.True(roomViewModel?.InGameUsers.First(x => x.IsHost).IsCurrent);
        Assert.NotNull(roomViewModel?.InGameUsers.First(x => x.IsHost).StartStepTime);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Equal(2, db.InGameUsers.Count());
        Assert.Empty(db.Animals);
        Assert.Single(db.Rooms);
        Assert.Equal(2, db.Users.Count());
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

        var userCreateCommand2 = new UserCreateCommand("test_user2", "123test");
        await mediator.Send(userCreateCommand2);

        var command2 = new UserLoginCommand("test_user2", "123test");
        var userView2 = await mediator.Send(command2);

        var createCommand = new RoomCreateCommand("test room", new(userView.UserName));
        var roomViewModel = await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(roomViewModel.Uid, new(userView2.UserName));
        await mediator.Send(enterCommand);

        var startCommand = new StartGameCommand(roomViewModel.Uid, new(userView.UserName));
        await mediator.Send(startCommand);

        var pauseCommand = new PauseGameCommand(roomViewModel.Uid, new(userView.UserName));
        await mediator.Send(pauseCommand);

        var resumeCommand = new ResumeGameCommand(roomViewModel.Uid, new(userView.UserName));
        await mediator.Send(resumeCommand);

        roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(roomViewModel.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Equal(2, roomViewModel?.InGameUsers.Count);
        Assert.Single(roomViewModel?.Additions);
        Assert.True(roomViewModel?.IsStarted);
        Assert.False(roomViewModel?.IsPaused);
        Assert.Null(roomViewModel?.FinishedDateTime);
        Assert.Null(roomViewModel?.PauseStartedTime);
        Assert.NotNull(roomViewModel?.StartDateTime);
        Assert.Equal(0, roomViewModel?.StepNumber);

        Assert.Equal(0, roomViewModel?.InGameUsers.First(x => x.IsHost).Order);
        Assert.True(roomViewModel?.InGameUsers.First(x => x.IsHost).IsCurrent);
        Assert.NotNull(roomViewModel?.InGameUsers.First(x => x.IsHost).StartStepTime);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Equal(2, db.InGameUsers.Count());
        Assert.Empty(db.Animals);
        Assert.Single(db.Rooms);
        Assert.Equal(2, db.Users.Count());
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

        var userCreateCommand2 = new UserCreateCommand("test_user2", "123test");
        await mediator.Send(userCreateCommand2);

        var command2 = new UserLoginCommand("test_user2", "123test");
        var userView2 = await mediator.Send(command2);

        var createCommand = new RoomCreateCommand("test room", new(userView.UserName));
        var roomViewModel = await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(roomViewModel.Uid, new(userView2.UserName));
        await mediator.Send(enterCommand);

        var startCommand = new StartGameCommand(roomViewModel.Uid, new(userView.UserName));
        await mediator.Send(startCommand);

        var pauseCommand = new PauseGameCommand(roomViewModel.Uid, new(userView.UserName));
        await mediator.Send(pauseCommand);

        var resumeCommand = new ResumeGameCommand(roomViewModel.Uid, new(userView.UserName));
        await mediator.Send(resumeCommand);

        var nextStepCommand = new NextStepCommand(roomViewModel.Uid);
        await mediator.Send(nextStepCommand);

        var endCommand = new EndGameCommand(roomViewModel.Uid);
        await mediator.Send(endCommand);

        roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(roomViewModel.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Equal(2, roomViewModel?.InGameUsers.Count);
        Assert.Single(roomViewModel?.Additions);
        Assert.False(roomViewModel?.IsStarted);
        Assert.False(roomViewModel?.IsPaused);
        Assert.NotNull(roomViewModel?.FinishedDateTime);
        Assert.Null(roomViewModel?.PauseStartedTime);
        Assert.NotNull(roomViewModel?.StartDateTime);
        Assert.Equal(1, roomViewModel?.StepNumber);

        Assert.Equal(0, roomViewModel?.InGameUsers.First(x => x.IsHost).Order);
        Assert.True(roomViewModel?.InGameUsers.First(x => !x.IsHost).IsCurrent);
        Assert.NotNull(roomViewModel?.InGameUsers.First(x => !x.IsHost).StartStepTime);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Equal(2, db.InGameUsers.Count());
        Assert.Empty(db.Animals);
        Assert.Single(db.Rooms);
        Assert.Equal(2, db.Users.Count());
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

        var userCreateCommand2 = new UserCreateCommand("test_user2", "123test");
        await mediator.Send(userCreateCommand2);

        var command2 = new UserLoginCommand("test_user2", "123test");
        var userView2 = await mediator.Send(command2);

        var createCommand = new RoomCreateCommand("test room", new(userView.UserName));
        var roomViewModel = await mediator.Send(createCommand);

        var enterCommand = new RoomEnterCommand(roomViewModel.Uid, new(userView2.UserName));
        await mediator.Send(enterCommand);

        var startCommand = new StartGameCommand(roomViewModel.Uid, new(userView.UserName));
        await mediator.Send(startCommand);

        var pauseCommand = new PauseGameCommand(roomViewModel.Uid, new(userView.UserName));
        await mediator.Send(pauseCommand);

        var resumeCommand = new ResumeGameCommand(roomViewModel.Uid, new(userView.UserName));
        await mediator.Send(resumeCommand);

        var nextStepCommand = new NextStepCommand(roomViewModel.Uid);
        await mediator.Send(nextStepCommand);

        roomViewModel = _services.Get<RoomQueries>().GetRoomViewModel(roomViewModel.Uid);

        Assert.NotNull(roomViewModel);
        Assert.Equal(2, roomViewModel?.InGameUsers.Count);
        Assert.Single(roomViewModel?.Additions);
        Assert.True(roomViewModel?.IsStarted);
        Assert.False(roomViewModel?.IsPaused);
        Assert.Null(roomViewModel?.FinishedDateTime);
        Assert.Null(roomViewModel?.PauseStartedTime);
        Assert.NotNull(roomViewModel?.StartDateTime);
        Assert.Equal(1, roomViewModel?.StepNumber);

        Assert.Equal(0, roomViewModel?.InGameUsers.First(x => x.IsHost).Order);
        Assert.True(roomViewModel?.InGameUsers.First(x => !x.IsHost).IsCurrent);
        Assert.NotNull(roomViewModel?.InGameUsers.First(x => !x.IsHost).StartStepTime);

        var db = _services.Get<EvolutionDbContext>();
        Assert.Equal(2, db.InGameUsers.Count());
        Assert.Empty(db.Animals);
        Assert.Single(db.Rooms);
        Assert.Equal(2, db.Users.Count());
        Assert.Single(db.Additions);
    }

    public void Dispose()
    {
        _services.Dispose();
    }
}
