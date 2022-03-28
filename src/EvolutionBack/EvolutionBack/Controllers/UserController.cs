using EvolutionBack.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvolutionBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Authorize(AuthPolicies.User)]
        public async Task Login()
        {
            throw new NotImplementedException();
        }

        [Authorize(AuthPolicies.Guest)]
        public async Task Register()
        {
            throw new NotImplementedException();
        }
    }
}
