using Domain.Models;

namespace Domain.Repo;

public interface IPropertyRepo
{
    public Property? Find(Guid uid);

    public Property? FindByAssemblyName(string name);

    public Property Create(Guid uid, string name, bool isPair, bool isOnEnemy, int? feedIncreasing, string assemblyName);

    public bool Remove(Guid uid);
}
