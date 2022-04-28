namespace EvolutionBack.Resources;

#pragma warning disable CS8618
public class CardJson
{
    public IList<CardJsonElement> Cards { get; set; }
}

public class CardJsonElement
{
    public int AdditionId { get; set; }

    public string FirstPropertyName { get; set; }

    public string? SecondPropertyName { get; set; }

    public int Count { get; set; }
}