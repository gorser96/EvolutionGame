using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class User : IdentityUser<Guid>
{
    public User(string userName, Guid Id)
    {
        UserName = userName;
        base.Id = Id;
    }

    public virtual InGameUser? InGameUser { get; private set; }

    // TODO: очки, рейтинг
}
