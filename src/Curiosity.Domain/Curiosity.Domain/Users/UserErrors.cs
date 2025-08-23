
using Curiosity.Domain.Abstractions;

namespace Curiosity.Domain.Users;
/// <summary>
/// Provides predefined error instances related to user operations.
/// </summary>
/// <remarks>This class contains static readonly fields representing common user-related errors,  such as when a
/// user is not found or when invalid credentials are provided. These  errors can be used consistently across the
/// application to represent specific failure  scenarios.</remarks>
public static class UserErrors
{
    public static readonly Error NotFound = new(
        "User.Found",
        "The user with the specified identifier was not found");

    public static readonly Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "The provided credentials were invalid");
}
