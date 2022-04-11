using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class RoomCreateCommand : IRequest<RoomViewModel>
{
    public RoomCreateCommand(Guid uid, string name)
    {
        Uid = uid;
        Name = name;
    }

    public Guid Uid { get; init; }

    public string Name { get; init; }
}
