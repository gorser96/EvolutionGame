using Domain.Models;

namespace Domain.Repo;

public interface IAdditionRepo
{
    public Addition? Find(Guid uid);

    public Addition Create(Guid uid, string name);

    public bool Remove(Guid uid);
}
