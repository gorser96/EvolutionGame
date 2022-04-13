using EvolutionBack.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvolutionBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        [Authorize(AuthPolicies.CreateRoom)]
        public async Task Create()
        {
            throw new NotImplementedException();
        }

        [Authorize(AuthPolicies.EnterRoom)]
        public async Task Enter()
        {
            throw new NotImplementedException();
        }

        [Authorize(AuthPolicies.ViewRoom)]
        public async Task EnterViewer()
        {
            throw new NotImplementedException();
        }

        [Authorize(AuthPolicies.CreateRoom)]
        public async Task Remove()
        {
            throw new NotImplementedException();
        }
    }
}
