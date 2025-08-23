namespace Curiosity.Domain.Users;

public readonly record struct UserId(string Value)
{
    public static UserId New() => new($"usr_{Guid.CreateVersion7()}");
}
