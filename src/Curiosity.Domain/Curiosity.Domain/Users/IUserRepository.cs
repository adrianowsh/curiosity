namespace Curiosity.Domain.Users;

/// <summary>
/// Defines a repository for managing user entities.
/// </summary>
/// <remarks>This interface provides methods for retrieving and adding user entities.  Implementations are
/// responsible for handling the underlying data storage and retrieval mechanisms.</remarks>
public interface IUserRepository
{
    Task<User?> TryGetByIdAsync(UserId id, CancellationToken cancellationToken = default);
    void Add(User user);
}
