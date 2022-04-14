using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class RoomUpdateCommand : IRequest<RoomViewModel>
{
    public RoomUpdateCommand(RoomEditModel editModel, UserCredentials user, Guid roomUid)
    {
        EditModel = editModel;
        User = user;
        RoomUid = roomUid;
    }

    public Guid RoomUid { get; set; }

    public RoomEditModel EditModel { get; init; }

    public UserCredentials User { get; init; }
}
