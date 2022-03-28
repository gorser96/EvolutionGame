using EvolutionBack.Models;
using EvolutionBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace EvolutionBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        [HttpGet(Name = "GetAnimals")]
        public IEnumerable<Animal> Get([FromServices] AnimalQueries animalQueries)
        {
            return animalQueries.GetAnimals();
        }
    }
}
