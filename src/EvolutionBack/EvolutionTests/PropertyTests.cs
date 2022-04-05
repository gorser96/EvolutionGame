using Domain.Repo;
using EvolutionTests.TestServices;
using Xunit;

namespace EvolutionTests;

public class PropertyTests
{
    private readonly WebServiceTest _services;

    public PropertyTests()
    {
        _services = new WebServiceTest();
    }

    [Fact]
    public void Burrowing_test()
    {
        var animalRepo = _services.Get<IAnimalRepo>();
    }

    [Fact]
    public void Carnivorous_test()
    {
    }
}
