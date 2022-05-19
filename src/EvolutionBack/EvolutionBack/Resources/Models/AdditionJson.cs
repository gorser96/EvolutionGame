namespace EvolutionBack.Resources;

#pragma warning disable CS8618
public class AdditionJson
{
    public IList<AdditionJsonElement> Additions { get; set; }
}

public class AdditionJsonElement
{
    public int Id { get; set; }
    public bool IsBase { get; set; }
    public string? IconName { get; set; }
}