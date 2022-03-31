using EvolutionBack.Domain.Models;

namespace EvolutionBack.Domain.Repo;

public interface IUserRepo
{
    public User Find(Guid uid);

    public User Create(Guid uid);

    public bool Remove(Guid uid);
}
