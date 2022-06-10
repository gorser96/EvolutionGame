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

    public async Task Handle(RoomModifiedEvent notification, CancellationToken cancellationToken)
    {
        await _publisher.RoomEvent(new(notification.Entity.Uid, Models.RoomIntegrationType.Modified));
    }
}
