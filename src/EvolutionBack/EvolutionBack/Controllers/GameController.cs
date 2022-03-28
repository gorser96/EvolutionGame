using EvolutionBack.Models;
using EvolutionBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace EvolutionBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : Controller
    {
        [HttpGet(Name = "GetAnimals")]
        public IEnumerable<Animal> Get([FromServices] AnimalQueries animalQueries)
        {
            return animalQueries.GetAnimals();
        }
    }
}
