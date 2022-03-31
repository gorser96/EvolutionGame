namespace Infrastructure.Models;

internal class Room : EvolutionBack.Domain.Models.Room
{
    public Room(Guid uid, string name) : base(uid, name)
    {
    }
}
