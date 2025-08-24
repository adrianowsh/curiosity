using Curiosity.Application.Authentication;
using Microsoft.AspNetCore.Http;

namespace Curiosity.Infrastructure.Authentication;

internal sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public string UserId =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new InvalidOperationException("User ID not found in the context.");

    public string IdentityId =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetIdentityId() ??
        throw new InvalidOperationException("Identity ID not found in the context.");
}

