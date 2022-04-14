namespace EvolutionBack.Core;

public class UserAlreadyRegisteredException : Exception
{
    public UserAlreadyRegisteredException() : base("User already registered!")
    {
    }
}
