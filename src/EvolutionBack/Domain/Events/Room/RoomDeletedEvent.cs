using Domain.Models;
using MediatR;

namespace Domain.Events;

public class RoomDeletedEvent : INotification
{
    public RoomDeletedEvent(Room entity)
    {
        Entity = entity;
    }

    public Room Entity { get; init; }
}
