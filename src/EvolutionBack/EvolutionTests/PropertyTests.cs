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
    public void Burrowing_test()
    {
    }

    [Fact]
    public void Carnivorous_test()
    {
    }

    public void Dispose()
    {
        _services.Dispose();
    }
}
