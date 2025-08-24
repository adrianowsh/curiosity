using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Curiosity.Infrastructure.Authentication;
internal static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);

        return string.IsNullOrEmpty(userId)
            ? throw new ApplicationException("User id is unavailable")
            : userId;
    }

    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.NameIdentifier) ??
               throw new ApplicationException("User identity is unavailable");
    }
}
