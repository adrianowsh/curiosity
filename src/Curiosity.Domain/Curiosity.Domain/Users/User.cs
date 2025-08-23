using Curiosity.Domain.Abstractions;

namespace Curiosity.Domain.Users;

/// <summary>
/// User entity.
/// </summary>
public sealed class User : Entity<UserId>
{
    private User(UserId id, Name name, Email email, Status status)
        : base(id)
    {
        Name = name;
        Email = email;
        Status = status;
    }

    private User()
    {
    }

    public Name Name { get; private set; }

    public Email Email { get; private set; }

    public Status Status { get; private set; }
    public string IdentityId { get; private set; } = string.Empty;

    public static User Create(Name firstName, Email email, Status status)
    {
        var user = new User(UserId.New(), firstName, email, status);

        return user;
    }

    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}
