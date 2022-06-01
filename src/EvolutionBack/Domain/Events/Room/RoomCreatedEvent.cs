using Domain.Models;
using MediatR;

namespace Domain.Events;

public class RoomCreatedEvent : INotification
{
    public RoomCreatedEvent(Room entity)
    {
        Entity = entity;
    }

    public Room Entity { get; init; }
}
