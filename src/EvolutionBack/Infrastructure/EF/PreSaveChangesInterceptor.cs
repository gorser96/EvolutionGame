using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.EF;

public class PreSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IMediator _mediator;
    private const int _dispatchCycleLimit = 50;

    public PreSaveChangesInterceptor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchEvents(eventData).Wait();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchEvents(eventData);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchEvents(DbContextEventData eventData)
    {
        if (eventData.Context is null)
        {
            return;
        }

        foreach (var _ in Enumerable.Range(0, _dispatchCycleLimit))
        {
            var domainEntities = eventData.Context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.GetAndClearEvents())
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }

            var hasNewEvents = eventData.Context.ChangeTracker.Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).Any();

            if (!hasNewEvents)
            {
                return;
            }
        }

        throw new ApplicationException("Dispatch cycle limit exceeded");
    }
}
