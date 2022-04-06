namespace Domain.Models;

public class User
{
    public User(string login, string password, Guid uid)
    {
        Login = login;
        Password = password;
        Uid = uid;
    }

    public Guid Uid { get; private set; }
    
    public string Login { get; private set; }

    public string Password { get; private set; }

    public virtual InGameUser? InGameUser { get; private set; }

    // TODO: очки, рейтинг
}
