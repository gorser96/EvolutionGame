using Domain.Events;
using EvolutionBack.Services;
using MediatR;

namespace EvolutionBack.EventHandlers;

public class RoomDeletedEventHandlerForMqEvent : INotificationHandler<RoomDeletedEvent>
{
    private readonly HubPublisher _publisher;

    public RoomDeletedEventHandlerForMqEvent(HubPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Handle(RoomDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _publisher.DeletedRoom(notification.Entity.Uid);
    }
}
