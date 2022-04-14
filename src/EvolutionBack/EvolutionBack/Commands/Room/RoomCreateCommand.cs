using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class RoomCreateCommand : IRequest<RoomViewModel>
{
    public RoomCreateCommand(Guid uid, string name, Guid userUid)
    {
        Uid = uid;
        Name = name;
        UserUid = userUid;
    }

    public Guid Uid { get; init; }

    public string Name { get; init; }

    public Guid UserUid { get; init; }
}
