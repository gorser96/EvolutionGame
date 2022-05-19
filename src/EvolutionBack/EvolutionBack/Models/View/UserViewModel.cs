namespace EvolutionBack.Models;

#pragma warning disable CS8618
public record UserViewModel
{
    public Guid Id { get; init; }

    public string UserName { get; init; }
}
#pragma warning restore CS8618
