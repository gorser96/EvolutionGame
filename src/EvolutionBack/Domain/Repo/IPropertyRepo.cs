using Domain.Models;

namespace Domain.Repo;

public interface IPropertyRepo
{
    public IProperty Find(Guid uid);

    public IProperty Create(Guid uid);

    public bool Remove(Guid uid);
}
