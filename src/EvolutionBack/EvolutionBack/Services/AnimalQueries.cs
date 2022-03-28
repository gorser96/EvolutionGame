using EvolutionBack.Models;

namespace EvolutionBack.Services;

public class AnimalQueries
{
    public IEnumerable<Animal> GetAnimals()
    {
        return Enumerable.Range(1, 5).Select(index => new Animal($"animal {index}")).ToArray();
    }
}
