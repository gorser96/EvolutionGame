using Domain.Models;

namespace Domain.Repo;

public interface IAdditionRepo
{
    public Addition? Find(Guid uid);

    public Addition Create(Guid uid, string name, bool isBase);

    public Addition? GetBaseAddition();

    public bool Remove(Guid uid);
}
