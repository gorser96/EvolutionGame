using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class UserLoginCommand : IRequest<UserViewModel>
{
    public UserLoginCommand(string login, string password)
    {
        Login = login;
        Password = password;
    }

    public string Login { get; private set; }

    /// <summary>
    /// Hash of password
    /// </summary>
    public string Password { get; private set; }
}
