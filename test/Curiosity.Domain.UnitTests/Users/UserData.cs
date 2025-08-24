using Curiosity.Domain.Users;

namespace Curiosity.Domain.UnitTests.Users;

internal static class UserData
{
    public static readonly Name  Name = new("John");
    public static readonly Status Active = Status.Active;
    public static readonly Status Inactive = Status.Inactive;
    public static readonly Email Email = new("johndoe@email.com");
}
