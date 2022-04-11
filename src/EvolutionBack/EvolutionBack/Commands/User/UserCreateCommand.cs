using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class UserCreateCommand : IRequest<UserViewModel>
{
    public UserCreateCommand(string login, string password)
    {
        Login = login;
        Password = password;
    }
    
    public string Login { get; set; }

    public string Password { get; set; }
}
