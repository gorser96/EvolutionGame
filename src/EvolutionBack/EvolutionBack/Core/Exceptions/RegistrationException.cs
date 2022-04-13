using Microsoft.AspNetCore.Identity;

namespace EvolutionBack.Core;

public class RegistrationException : Exception
{
    public RegistrationException(IEnumerable<IdentityError> errors)
    {
        Errors = errors;
    }

    public IEnumerable<IdentityError> Errors { get; }
}
