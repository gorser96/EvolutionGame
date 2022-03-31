using EvolutionBack.Domain.Models;

namespace EvolutionBack.Domain.Repo;

public interface IRoomRepo
{
    public Room Find(Guid uid);

    public Room Create(Guid uid);

    public bool Remove(Guid uid);
}
