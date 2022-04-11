using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class RoomUpdateCommand : IRequest<RoomViewModel>
{
    public RoomUpdateCommand(RoomEditModel editModel)
    {
        EditModel = editModel;
    }

    public RoomEditModel EditModel { get; init; }
}
