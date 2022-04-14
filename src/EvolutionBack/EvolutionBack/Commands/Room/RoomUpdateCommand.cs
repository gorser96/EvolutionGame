using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class RoomUpdateCommand : IRequest<RoomViewModel>
{
    public RoomUpdateCommand(RoomEditModel editModel, Guid userUid)
    {
        EditModel = editModel;
        UserUid = userUid;
    }

    public RoomEditModel EditModel { get; init; }

    public Guid UserUid { get; init; }
}
