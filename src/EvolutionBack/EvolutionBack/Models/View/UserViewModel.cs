namespace EvolutionBack.Models;

public class UserViewModel
{
    public UserViewModel(string login, Guid uid)
    {
        Login = login;
        Uid = uid;
    }

    public Guid Uid { get; private set; }

    public string Login { get; private set; }
}
