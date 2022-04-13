namespace EvolutionBack.Models;

public class UserViewModel
{
    public UserViewModel(string userName, Guid uid)
    {
        UserName = userName;
        Uid = uid;
    }

    public UserViewModel(string userName, Guid uid, string token, DateTime expiration) : this(userName, uid)
    {
        Token = token;
        Expiration = expiration;
    }

    public Guid Uid { get; private set; }

    public string? Token { get; private set; }

    public DateTime? Expiration { get; private set; }

    public string UserName { get; private set; }
}
