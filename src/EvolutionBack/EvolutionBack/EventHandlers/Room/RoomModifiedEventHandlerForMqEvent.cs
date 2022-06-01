using Domain.Events;
using EvolutionBack.Services;
using MediatR;

namespace EvolutionBack.EventHandlers;

public class RoomModifiedEventHandlerForMqEvent : INotificationHandler<RoomModifiedEvent>
{
    private readonly HubPublisher _publisher;

    public RoomModifiedEventHandlerForMqEvent(HubPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task Handle(RoomModifiedEvent notification, CancellationToken cancellationToken)
    {
        _publisher.UpdateRoom(notification.Entity.Uid);
        return Task.CompletedTask;
    }
}
