using Domain.Models;
using Domain.Repo;

namespace Infrastructure.Repo;

public class AnimalRepo : IAnimalRepo
{
    public Animal Create(Guid uid, Guid userUid)
    {
        throw new NotImplementedException();
    }

    public Animal Find(Guid uid)
    {
        throw new NotImplementedException();
    }

    public bool Remove(Guid uid)
    {
        throw new NotImplementedException();
    }
}
