namespace Curiosity.Api.Controllers.Users;

public sealed record RegisterUserRequest(
    string Email,
    string Name,
    string Password);
