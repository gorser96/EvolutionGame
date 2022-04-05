using Domain.Models;

namespace Domain.Repo;

public interface IRoomRepo
{
    public Room Find(Guid uid);

    public Room Create(Guid uid);

    public bool Remove(Guid uid);
}
