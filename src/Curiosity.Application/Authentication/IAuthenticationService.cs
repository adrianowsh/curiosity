using Curiosity.Domain.Users;

namespace Curiosity.Application.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(
        User user,
        string password,
        CancellationToken cancellationToken);
}
