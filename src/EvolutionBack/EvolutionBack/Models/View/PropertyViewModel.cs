namespace EvolutionBack.Models;

#pragma warning disable CS8618
public record PropertyViewModel
{
    public Guid Uid { get; init; }

    public string Name { get; private set; }

    public bool IsPair { get; init; }

    public bool IsOnEnemy { get; init; }

    public int? FeedIncreasing { get; init; }
}
#pragma warning restore CS8618
