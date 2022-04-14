namespace EvolutionBack.Models;

public class UserCredentials
{
    public UserCredentials(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; init; }
}
