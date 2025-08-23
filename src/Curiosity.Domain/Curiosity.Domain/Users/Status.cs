namespace Curiosity.Domain.Users;

/// <summary>
/// Represents the status of an operation or entity as a boolean value.
/// </summary>
/// <remarks>The <see cref="Status"/> type provides a simple way to represent active or inactive states. Use the
/// predefined static properties <see cref="Active"/> and <see cref="Inactive"/> for common scenarios.</remarks>
/// <param name="Value"></param>
public readonly record struct Status(bool Value)
{
    public static Status Active => new(true);

    public static Status Inactive => new(false);
}
