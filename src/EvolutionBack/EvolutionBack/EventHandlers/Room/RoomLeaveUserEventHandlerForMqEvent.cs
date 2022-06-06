using Domain.Events;
using EvolutionBack.Services;
using MediatR;

namespace EvolutionBack.EventHandlers;

public class RoomLeaveUserEventHandlerForMqEvent : INotificationHandler<RoomLeaveUserEvent>
{
    private readonly HubPublisher _publisher;

    public RoomLeaveUserEventHandlerForMqEvent(HubPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Handle(RoomLeaveUserEvent notification, CancellationToken cancellationToken)
    {
        await _publisher.LeaveRoom(notification.UserName, notification.Entity.Uid);
        await _publisher.UpdatedRoom(notification.Entity.Uid);
    }
}
