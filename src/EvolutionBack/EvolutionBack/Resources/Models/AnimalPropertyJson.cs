namespace EvolutionBack.Resources;

#pragma warning disable CS8618
public class AnimalPropertyJson
{
    public IList<AnimalPropertyJsonElement> Properties { get; set; }
}

public class AnimalPropertyJsonElement
{
    public string AssemblyName { get; set; }

    public bool IsPair { get; set; }

    public bool IsOnEnemy { get; set; }
}