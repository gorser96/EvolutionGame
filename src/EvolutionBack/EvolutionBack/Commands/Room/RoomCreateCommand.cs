using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class RoomCreateCommand : IRequest<RoomViewModel>
{
    public RoomCreateCommand(string name, UserCredentials user)
    {
        Name = name;
        User = user;
    }

    public string Name { get; init; }

    public UserCredentials User { get; init; }
}
