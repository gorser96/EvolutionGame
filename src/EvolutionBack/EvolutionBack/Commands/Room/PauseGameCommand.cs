using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class PauseGameCommand : IRequest
{
    public PauseGameCommand(Guid roomUid, UserCredentials user)
    {
        RoomUid = roomUid;
        User = user;
    }

    public Guid RoomUid { get; init; }
    
    public UserCredentials User { get; init; }
}
