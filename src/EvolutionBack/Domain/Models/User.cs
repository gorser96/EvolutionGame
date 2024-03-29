﻿using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

/// <summary>
/// Учетная запись игрока
/// </summary>
public class User : IdentityUser<Guid>
{
    public User(string userName, Guid Id)
    {
        UserName = userName;
        base.Id = Id;
    }

    public virtual InGameUser? InGameUser { get; private set; }

    public virtual GameHistoryUser? GameHistoryUser { get; private set; }
}
