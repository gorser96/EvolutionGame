﻿namespace EvolutionBack.Models;

public class UserViewModel
{
    public UserViewModel(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; init; }
}
