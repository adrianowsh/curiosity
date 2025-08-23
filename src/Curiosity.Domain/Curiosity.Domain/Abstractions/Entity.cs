namespace Curiosity.Domain.Abstractions;

/// <summary>
/// Entity base class.
/// </summary>
public abstract class Entity<TEntityId>: IEntity
    where TEntityId : notnull
{
    private readonly IList<IDomainEvent> _domainEvents = [];

    protected Entity(TEntityId id)
    {
        Id = id;
        InsertedAt = DateTime.UtcNow;
    }

    protected Entity()
    {
    }

    public TEntityId Id { get; init; }

    public DateTime InsertedAt { get; init; }

    public DateTime? UpdatedAt { get; set; }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
