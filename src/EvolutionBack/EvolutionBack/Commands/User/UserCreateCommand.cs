using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EvolutionBack.Commands;

public class UserCreateCommand : IRequest
{
    public UserCreateCommand(string login, string password)
    {
        Login = login;
        Password = password;
    }

    [Required(ErrorMessage = "Login is required")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
