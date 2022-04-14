using EvolutionBack.Commands;
using EvolutionBack.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EvolutionBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<UserTokenViewModel> Login([FromBody] UserLoginCommand loginCommand, [FromServices] IMediator mediator)
        {
            var user = await mediator.Send(loginCommand);

            return user;
        }

        [HttpPost("register")]
        public async Task Register([FromBody] UserCreateCommand createCommand, [FromServices] IMediator mediator)
        {
            await mediator.Send(createCommand);
        }
    }
}
