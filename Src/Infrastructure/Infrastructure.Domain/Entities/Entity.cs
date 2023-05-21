namespace Infrastructure.Domain.Entities;

using Events;

public abstract class Entity : IEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    
    public virtual long Id { get; init; }
    
    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents.AsEnumerable();

    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        if (domainEvent is null)
            throw new ArgumentNullException(nameof(domainEvent));
        
        _domainEvents.Add(domainEvent);
    }
}