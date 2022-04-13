using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class User : IdentityUser
{
    public User(string userName, string Id)
    {
        UserName = userName;
        base.Id = Id;
    }

    public Guid Uid => Guid.Parse(Id);

    public virtual InGameUser? InGameUser { get; private set; }

    // TODO: очки, рейтинг
}
