﻿using Domain.Models;

namespace Domain.Repo;

public interface IUserRepo
{
    public User Find(Guid uid);

    public User Create(Guid uid);

    public bool Remove(Guid uid);
}
