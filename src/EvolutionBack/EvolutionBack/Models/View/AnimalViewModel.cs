namespace EvolutionBack.Models;

#pragma warning disable CS8618
public record AnimalViewModel
{
    public Guid Uid { get; private set; }

    public int FoodCurrent { get; private set; }

    public int FoodMax { get; private set; }

    public ICollection<InAnimalPropertyViewModel> Properties { get; private set; }
}
#pragma warning restore CS8618
