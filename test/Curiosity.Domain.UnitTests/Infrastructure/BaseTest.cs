using Curiosity.Domain.Abstractions;

namespace Curiosity.Domain.UnitTests.Infrastructure;

public abstract class BaseTest
{
    public static T AssertDomainEventWasPublished<T>(IEntity entity)
        where T : IDomainEvent
    {
        T? domainEvent = entity.GetDomainEvents().OfType<T>().SingleOrDefault();

        if (domainEvent is null)
        {
            throw new InvalidOperationException($"{typeof(T).Name} was not published");
        }

        return domainEvent;
    }
}
