namespace EvolutionBack.Models;

#pragma warning disable CS8618
public record CardViewModel
{
    public Guid Uid { get; init; }

    public virtual PropertyViewModel FirstProperty { get; init; }

    public virtual PropertyViewModel? SecondProperty { get; init; }
}
#pragma warning restore CS8618
