namespace EvolutionBack.Models;

public class InGameUserViewModel
{
    public InGameUserViewModel(UserViewModel user)
    {
        User = user;
    }

    public UserViewModel User { get; private set; }
}
