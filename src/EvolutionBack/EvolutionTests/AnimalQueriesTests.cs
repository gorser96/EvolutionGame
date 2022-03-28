using EvolutionBack.Services;
using System.Linq;
using Xunit;

namespace EvolutionTests;

public class AnimalQueriesTests
{
    [Fact]
    public void Get_animals()
    {
        // создаем сервис
        var animalQueries = new AnimalQueries();
        // вызываем метод сервиса
        var animals = animalQueries.GetAnimals().ToList();
        // проверяем полученный список
        Assert.Equal(5, animals.Count);
        for (int i = 0; i < animals.Count; i++)
        {
            // проверяем свойства каждого элемента
            Assert.Equal($"animal {i + 1}", animals[i].Name);
        }
    }
}
