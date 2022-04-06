using Domain.Models;

namespace Domain.Repo;

public interface IPropertyRepo
{
    public Property Find(Guid uid);

    public Property Create(Guid uid);

    public bool Remove(Guid uid);
}
