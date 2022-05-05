namespace EvolutionBack.Models;

#pragma warning disable CS8618
public record InAnimalPropertyViewModel
{
    public Guid PropertyUid { get; init; }

    public PropertyViewModel Property { get; init; }

    public bool IsActive { get; private set; }
}
#pragma warning restore CS8618
