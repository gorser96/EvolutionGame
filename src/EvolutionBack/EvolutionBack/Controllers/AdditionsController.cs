using EvolutionBack.Core;
using EvolutionBack.Models;
using EvolutionBack.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvolutionBack.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AdditionsController : ControllerBase
{
    [HttpGet("list")]
    public ICollection<AdditionViewModel> GetAdditions([FromServices] AdditionQueries additionQueries)
    {
        return additionQueries.GetAdditions();
    }

    [HttpGet("{uid:guid}")]
    public AdditionViewModel Get(Guid uid, [FromServices] AdditionQueries additionQueries)
    {
        var addition = additionQueries.GetAddition(uid);
        if (addition is null)
        {
            throw new ObjectNotFoundException(uid, nameof(addition));
        }
        return addition;
    }
}
