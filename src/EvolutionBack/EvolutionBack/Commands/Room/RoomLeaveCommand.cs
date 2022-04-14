﻿using EvolutionBack.Models;
using MediatR;

namespace EvolutionBack.Commands;

public class RoomLeaveCommand : IRequest<RoomViewModel>
{
    public RoomLeaveCommand(Guid roomUid, UserCredentials user)
    {
        RoomUid = roomUid;
        User = user;
    }

    public Guid RoomUid { get; init; }

    public UserCredentials User { get; init; }
}
