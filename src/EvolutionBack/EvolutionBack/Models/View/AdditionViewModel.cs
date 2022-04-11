namespace EvolutionBack.Models;

public class AdditionViewModel
{
    public AdditionViewModel(Guid uid, string name)
    {
        Uid = uid;
        Name = name;
    }

    public Guid Uid { get; private set; }

    public string Name { get; private set; }
}
