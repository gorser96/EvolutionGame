using Microsoft.AspNetCore.Identity;

namespace EvolutionBack.Core;

public class RegistrationException : Exception
{
    public RegistrationException(IEnumerable<IdentityError> errors) : base(string.Join('\n', errors.Select(x => $"[{x.Code}] {x.Description}")))
    {
    }
}
