using Curiosity.Domain.Users;

namespace Curiosity.Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationDbContext dbContext) :
    Repository<User, UserId>(dbContext), IUserRepository
{
    public override void Add(User user)
    {
        DbContext.Add(user);
    }
}
