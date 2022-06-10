using Domain.Models;
using MediatR;

namespace Domain.Events;

public class RoomLeaveUserEvent : INotification
{
    public RoomLeaveUserEvent(Room entity, string userName)
    {
        Entity = entity;
        UserName = userName;
    }

    public Room Entity { get; init; }

    public string UserName { get; init; }
}
