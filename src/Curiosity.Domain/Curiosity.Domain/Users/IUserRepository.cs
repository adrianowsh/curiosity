namespace Curiosity.Domain.Users;

public interface IUserRepository
{
    Task<User?> TryGetByIdAsync(UserId id, CancellationToken cancellationToken = default);
    void Add(User user);
}
