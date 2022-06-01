using MediatR;

namespace Domain.Models;

public abstract class Entity
{
    private readonly List<INotification> _domainEvents = new();

    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public IList<INotification> GetAndClearEvents()
    {
        var result = _domainEvents.ToList();
        _domainEvents.Clear();
        return result;
    }
}
