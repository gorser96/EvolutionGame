using Domain.Events;
using EvolutionBack.Services;
using MediatR;

namespace EvolutionBack.EventHandlers;

public class RoomEnterUserEventHandlerForMqEvent : INotificationHandler<RoomEnterUserEvent>
{
    private readonly HubPublisher _publisher;

    public RoomEnterUserEventHandlerForMqEvent(HubPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Handle(RoomEnterUserEvent notification, CancellationToken cancellationToken)
    {
        await _publisher.JoinToRoom(notification.UserName, notification.Entity.Uid);
        await _publisher.RoomEvent(new(notification.Entity.Uid, Models.RoomIntegrationType.UserJoined));
    }
}
