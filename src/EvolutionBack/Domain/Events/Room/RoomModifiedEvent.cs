using Domain.Models;
using MediatR;

namespace Domain.Events;

public class RoomModifiedEvent : INotification
{
    public RoomModifiedEvent(Room entity)
    {
        Entity = entity;
    }

    public Room Entity { get; init; }
}
