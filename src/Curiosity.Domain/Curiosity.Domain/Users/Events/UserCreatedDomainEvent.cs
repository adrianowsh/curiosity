using Curiosity.Domain.Abstractions;

namespace Curiosity.Domain.Users.Events;

/// <summary>
/// Represents a domain event that is triggered when a new user is created.
/// </summary>
/// <remarks>This event is typically used to notify other parts of the system about the creation of a new user. It
/// contains the unique identifier of the created user.</remarks>
/// <param name="UserId">The unique identifier of the user that was created. This value cannot be null.</param>
public record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;
