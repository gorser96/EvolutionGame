using Domain.Models;
using EvolutionTests.TestServices;
using System;
using Xunit;

namespace EvolutionTests;

public class PropertyTests : IDisposable
{
    private readonly WebServiceTest _services;

    public PropertyTests()
    {
        _services = new();
    }



    [Fact]
    public void Burrowing_test_false()
    {
        var user1Uid = Guid.NewGuid();
        var roomUid = Guid.NewGuid();
        var selfAnimal = new Animal(Guid.NewGuid(), user1Uid, roomUid);
        var enemyAnimal = new Animal(Guid.NewGuid(), user1Uid, roomUid);
        var burrowing = new Burrowing(Guid.NewGuid(), "Норное", false, false);

        var result = burrowing.OnDefense(selfAnimal, enemyAnimal);

        Assert.NotNull(result);
        Assert.False(result);
    }
    [Fact]
    public void Burrowing_test_true()
    {
        var user1Uid = Guid.NewGuid();
        var roomUid = Guid.NewGuid();
        var defAnimal = new Animal(Guid.NewGuid(), user1Uid, roomUid);
        var enemyAnimal = new Animal(Guid.NewGuid(), user1Uid, roomUid);
        var burrowing = new Burrowing(Guid.NewGuid(), "Норное", false, false);

        defAnimal.AddProperty(burrowing);
        defAnimal.Feed(1);

        var result = burrowing.OnDefense(defAnimal, enemyAnimal);

        Assert.Equal(1, defAnimal.FoodMax);
        Assert.Equal(1, defAnimal.Properties.Count);
        Assert.NotNull(result);
        Assert.True(result);
    }

    [Fact]
    public void Carnivorous_test()
    {
        var user1Uid = Guid.NewGuid();
        var roomUid = Guid.NewGuid();
        var carnivorous_Animal = new Animal(Guid.NewGuid(), user1Uid, roomUid);
        var carnivorous = new Carnivorous(Guid.NewGuid(), "Хищник", false, false);

        carnivorous_Animal.AddProperty(carnivorous);

        Assert.Equal(1, carnivorous_Animal.Properties.Count);
        Assert.Equal(2, carnivorous_Animal.FoodMax);
    }

    public void Dispose()
    {
        _services.Dispose();
    }
}
