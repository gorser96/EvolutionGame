namespace EvolutionBack.Domain.Models;

public class User
{
    public User(string login, string password, Guid uid)
    {
        Login = login;
        Password = password;
        Uid = uid;
    }

    public Guid Uid { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public Guid? RoomUid { get; set; }

    // TODO: очки, рейтинг
}
