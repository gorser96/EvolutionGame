using Domain.Models;

namespace Domain.Repo;

public interface IAnimalRepo
{
    public Animal? Find(Guid uid);

    public Animal Create(Guid uid, Guid userUid);

    public bool Remove(Guid uid);
}
