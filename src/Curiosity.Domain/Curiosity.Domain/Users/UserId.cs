namespace Curiosity.Domain.Users;

/// <summary>
/// Represents a unique identifier for a user.
/// </summary>
/// <remarks>The <see cref="UserId"/> type encapsulates a string value that uniquely identifies a user.  Instances
/// of <see cref="UserId"/> are immutable and can be created using the <see cref="New"/> method.</remarks>
/// <param name="Value"></param>
public readonly record struct UserId(string Value)
{
    public static UserId New() => new($"usr_{Guid.CreateVersion7()}");
}
