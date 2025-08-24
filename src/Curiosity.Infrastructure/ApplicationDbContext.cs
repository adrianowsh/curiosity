using Curiosity.Application.Exceptions;
using Curiosity.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Curiosity.Infrastructure;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) : DbContext(options), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch (ConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred", ex);
        }
    }
}
