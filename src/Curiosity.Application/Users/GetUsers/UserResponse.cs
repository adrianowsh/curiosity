namespace Curiosity.Application.Users.GetUsers;

public sealed class UserResponse
{
    public string Id { get; init; }

    public string Email { get; init; }

    public string Name { get; init; }

    public bool Status { get; set; }
}
