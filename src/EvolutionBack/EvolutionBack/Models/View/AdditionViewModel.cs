namespace EvolutionBack.Models;

#pragma warning disable CS8618
public record AdditionViewModel
{
    public Guid Uid { get; init; }

    public string Name { get; init; }

    public bool IsBase { get; init; }

    public string? IconName { get; init; }

    public byte[]? Icon { get; init; }

    public ICollection<CardViewModel> Cards { get; init; }
}
#pragma warning restore CS8618
