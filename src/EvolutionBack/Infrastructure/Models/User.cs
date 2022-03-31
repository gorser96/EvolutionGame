namespace Infrastructure.Models;

internal class User : EvolutionBack.Domain.Models.User
{
    public User(string login, string password, Guid uid) : base(login, password, uid)
    {
    }
}