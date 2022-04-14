using EvolutionBack.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EvolutionBack.Commands;

public class UserLoginCommand : IRequest<UserTokenViewModel>
{
    public UserLoginCommand(string login, string password)
    {
        Login = login;
        Password = password;
    }

    [Required(ErrorMessage = "Login is required")]
    public string Login { get; private set; }

    /// <summary>
    /// Hash of password
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; private set; }
}
