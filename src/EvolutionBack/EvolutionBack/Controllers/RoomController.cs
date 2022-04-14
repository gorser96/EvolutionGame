using EvolutionBack.Commands;
using EvolutionBack.Core;
using EvolutionBack.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvolutionBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        [HttpPost]
        [Authorize(AuthPolicies.User)]
        public async Task<RoomViewModel> Create([FromBody] RoomCreateCommand command, [FromServices] IMediator mediator)
        {
            var roomViewModel = await mediator.Send(command);            
            return roomViewModel;
        }

        [HttpPost]
        [Authorize(AuthPolicies.User)]
        public async Task<RoomViewModel> Enter([FromBody] RoomEnterCommand command, [FromServices] IMediator mediator)
        {
            var roomViewModel = await mediator.Send(command);
            return roomViewModel;
        }

        [HttpPost]
        [Authorize(AuthPolicies.User)]
        public Task EnterViewer()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize(AuthPolicies.User)]
        public async Task<RoomViewModel> Update([FromBody] RoomUpdateCommand command, [FromServices] IMediator mediator)
        {
            var roomViewModel = await mediator.Send(command);
            return roomViewModel;
        }

        [HttpPost]
        [Authorize(AuthPolicies.User)]
        public async Task<RoomViewModel> Leave([FromBody] RoomLeaveCommand command, [FromServices] IMediator mediator)
        {
            var roomViewModel = await mediator.Send(command);
            return roomViewModel;
        }

        [HttpPost]
        [Authorize(AuthPolicies.User)]
        public async Task Start([FromBody] StartGameCommand command, [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
        }

        [HttpPost]
        [Authorize(AuthPolicies.User)]
        public async Task Pause([FromBody] PauseGameCommand command, [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
        }

        [HttpPost]
        [Authorize(AuthPolicies.User)]
        public async Task Resume([FromBody] ResumeGameCommand command, [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
        }

        [HttpPost]
        [Authorize(AuthPolicies.User)]
        public async Task End([FromBody] EndGameCommand command, [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
        }

        [HttpPost]
        [Authorize(AuthPolicies.User)]
        public async Task Remove([FromBody] RoomRemoveCommand command, [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
        }
    }
}
