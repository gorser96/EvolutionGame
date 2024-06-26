﻿using EvolutionBack.Commands;
using EvolutionBack.Core;
using EvolutionBack.Models;
using EvolutionBack.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvolutionBack.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RoomController : ControllerBase
{
    [HttpPost("create")]
    public async Task<RoomViewModel> Create([FromBody] RoomNameModel roomNameModel, [FromServices] IMediator mediator)
    {
        var user = User.Identity;
        if (user is null || user.Name is null)
        {
            throw new ApplicationException("User identity not found!");
        }

        var roomViewModel = await mediator.Send(new RoomCreateCommand(roomNameModel.Name, new(user.Name)));
        return roomViewModel;
    }

    [HttpGet("list")]
    public Task<ICollection<RoomViewModel>> List([FromServices] RoomQueries roomQueries)
    {
        return Task.FromResult(roomQueries.GetRooms());
    }

    [HttpGet("{uid:guid}")]
    public Task<RoomViewModel> Get(Guid uid, [FromServices] RoomQueries roomQueries)
    {
        var room = roomQueries.GetRoomViewModel(uid);
        if (room is null)
        {
            throw new ObjectNotFoundException(uid, nameof(room));
        }
        return Task.FromResult(room);
    }

    [HttpGet("{uid:guid}/users")]
    public Task<ICollection<InGameUserViewModel>> GetUsers(Guid uid, [FromServices] RoomQueries roomQueries)
    {
        return Task.FromResult(roomQueries.GetUsersFromRoom(uid));
    }

    [HttpGet("user")]
    public Task<RoomViewModel?> GetHostedRoom([FromServices] RoomQueries roomQueries)
    {
        var user = User.Identity;
        if (user is null || user.Name is null)
        {
            throw new ApplicationException("User identity not found!");
        }

        return Task.FromResult(roomQueries.GetRoomWithUser(user.Name));
    }

    [HttpPost("{roomUid:guid}/enter")]
    public async Task Enter(Guid roomUid, [FromServices] IMediator mediator)
    {
        var user = User.Identity;
        if (user is null || user.Name is null)
        {
            throw new ApplicationException("User identity not found!");
        }

        await mediator.Send(new RoomEnterCommand(roomUid, new(user.Name)));
    }

    [HttpPost("enter-viewer")]
    public Task EnterViewer()
    {
        throw new NotImplementedException();
    }

    [HttpPost("{roomUid:guid}/update")]
    public async Task<RoomViewModel> Update(Guid roomUid, [FromBody] RoomEditModel editModel, [FromServices] IMediator mediator)
    {
        var user = User.Identity;
        if (user is null || user.Name is null)
        {
            throw new ApplicationException("User identity not found!");
        }

        var roomViewModel = await mediator.Send(new RoomUpdateCommand(editModel, new(user.Name), roomUid));
        return roomViewModel;
    }

    [HttpPost("{roomUid:guid}/leave")]
    public async Task Leave(Guid roomUid, [FromServices] IMediator mediator)
    {
        var user = User.Identity;
        if (user is null || user.Name is null)
        {
            throw new ApplicationException("User identity not found!");
        }

        await mediator.Send(new RoomLeaveCommand(roomUid, new(user.Name)));
    }

    [HttpPost("{roomUid:guid}/kick/{userUid:guid}")]
    public async Task Kick(Guid roomUid, Guid userUid, [FromServices] IMediator mediator)
    {
        var user = User.Identity;
        if (user is null || user.Name is null)
        {
            throw new ApplicationException("User identity not found!");
        }

        await mediator.Send(new RoomKickCommand(roomUid, userUid, new(user.Name)));
    }

    [HttpPost("{roomUid:guid}/start")]
    public async Task Start(Guid roomUid, [FromServices] IMediator mediator)
    {
        var user = User.Identity;
        if (user is null || user.Name is null)
        {
            throw new ApplicationException("User identity not found!");
        }

        await mediator.Send(new StartGameCommand(roomUid, new(user.Name)));
    }

    [HttpPost("{roomUid:guid}/pause")]
    public async Task Pause(Guid roomUid, [FromServices] IMediator mediator)
    {
        var user = User.Identity;
        if (user is null || user.Name is null)
        {
            throw new ApplicationException("User identity not found!");
        }

        await mediator.Send(new PauseGameCommand(roomUid, new(user.Name)));
    }

    [HttpPost("{roomUid:guid}/resume")]
    public async Task Resume(Guid roomUid, [FromServices] IMediator mediator)
    {
        var user = User.Identity;
        if (user is null || user.Name is null)
        {
            throw new ApplicationException("User identity not found!");
        }

        await mediator.Send(new ResumeGameCommand(roomUid, new(user.Name)));
    }

    [HttpPost("{roomUid:guid}/end")]
    public async Task End(Guid roomUid, [FromServices] IMediator mediator)
    {
        var user = User.Identity;
        if (user is null || user.Name is null)
        {
            throw new ApplicationException("User identity not found!");
        }

        await mediator.Send(new EndGameCommand(roomUid));
    }

    [HttpPost("{roomUid:guid}/remove")]
    public async Task Remove(Guid roomUid, [FromServices] IMediator mediator)
    {
        var user = User.Identity;
        if (user is null || user.Name is null)
        {
            throw new ApplicationException("User identity not found!");
        }

        await mediator.Send(new RoomRemoveCommand(roomUid));
    }
}
