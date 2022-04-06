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
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Authorize(AuthPolicies.User)]
        public async Task<UserViewModel> Login([FromBody] UserLoginCommand loginCommand, [FromServices] IMediator mediator)
        {
            // TODO: передавать хеш из фронта
            var user = await mediator.Send(loginCommand);

            return user;
        }

        [HttpPost()]
        [Authorize(AuthPolicies.Guest)]
        public async Task<UserViewModel> Register([FromBody] UserCreateCommand createCommand, [FromServices] IMediator mediator)
        {
            var user = await mediator.Send(createCommand);
            return user;
        }
    }
}
