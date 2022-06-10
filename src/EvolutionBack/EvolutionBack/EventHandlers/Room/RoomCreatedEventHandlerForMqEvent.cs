using Domain.Events;
using EvolutionBack.Services;
using MediatR;

namespace EvolutionBack.EventHandlers;

public class RoomCreatedEventHandlerForMqEvent : INotificationHandler<RoomCreatedEvent>
{
    private readonly HubPublisher _publisher;

    public RoomCreatedEventHandlerForMqEvent(HubPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Handle(RoomCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _publisher.RoomEvent(new(notification.Entity.Uid, Models.RoomIntegrationType.Created));
    }
}
