namespace Curiosity.Domain.Abstractions;

/// <summary>
/// Interface for entities.
/// </summary>
public interface IEntity
{
    IReadOnlyList<IDomainEvent> GetDomainEvents();
    void ClearDomainEvents();
}
