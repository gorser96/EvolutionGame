using MediatR;

namespace Domain.Models;

/// <summary>
/// Абстрактный класс, который добавляет хранилище доменных событий для сущности
/// </summary>
public abstract class Entity
{
    private readonly List<INotification> _domainEvents = new();

    /// <summary>
    /// Хранилище доменных событий
    /// </summary>
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Добавляет доменное событие в хранилище
    /// </summary>
    /// <param name="eventItem"></param>
    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    /// <summary>
    /// Возвращает список доменных событий и очищает хранилище
    /// </summary>
    /// <returns></returns>
    public IList<INotification> GetAndClearEvents()
    {
        var result = _domainEvents.ToList();
        _domainEvents.Clear();
        return result;
    }
}
