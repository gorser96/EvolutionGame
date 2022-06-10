using Domain.Models;
using MediatR;

namespace Domain.Events;

public class RoomEnterUserEvent : INotification
{
    public RoomEnterUserEvent(Room entity, string userName)
    {
        Entity = entity;
        UserName = userName;
    }

    public Room Entity { get; init; }

    public string UserName { get; init; }
}
