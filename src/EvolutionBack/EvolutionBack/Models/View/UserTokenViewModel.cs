namespace EvolutionBack.Models;

public class UserTokenViewModel
{
    public UserTokenViewModel(string userName, string token, DateTime expiration)
    {
        Token = token;
        UserName = userName;
        Expiration = expiration;
    }

    public string Token { get; init; }

    public DateTime Expiration { get; init; }

    public string UserName { get; init; }
}
