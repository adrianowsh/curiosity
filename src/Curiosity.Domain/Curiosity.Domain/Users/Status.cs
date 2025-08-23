namespace Curiosity.Domain.Users;

public readonly record struct Status(bool Value)
{
    public static Status Active => new(true);

    public static Status Inactive => new(false);
}
