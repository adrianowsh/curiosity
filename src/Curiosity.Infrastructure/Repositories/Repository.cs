using Curiosity.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Curiosity.Infrastructure.Repositories;

internal abstract class Repository<T, TEntityId>(ApplicationDbContext dbContext)
    where T : Entity<TEntityId>
    where TEntityId : class 
{
    protected readonly ApplicationDbContext DbContext = dbContext;

    public virtual async Task<T?> TryGetByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<T>()
            .FirstOrDefaultAsync(e => e.Id == id , cancellationToken);
    }

    public virtual void Add(T entity)
    {
        DbContext.Add(entity);
    }
}
