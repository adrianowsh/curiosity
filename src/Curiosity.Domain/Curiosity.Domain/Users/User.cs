using Curiosity.Domain.Abstractions;
using Curiosity.Domain.Users.Events;

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

    /// <summary>
    /// private constructor for ORM.
    /// </summary>
    private User()
    {
    }

    /// <summary>
    /// Name of the user.
    /// </summary>
    public Name Name { get; private set; }

    /// <summary>
    /// Email of the user.
    /// </summary>
    public Email Email { get; private set; }

    /// <summary>
    /// Status of the user.
    /// </summary>
    public Status Status { get; private set; }

    /// <summary>
    /// Identity id in the identity provider.
    /// </summary>
    public string IdentityId { get; private set; } = string.Empty;


    /// <summary>
    /// Static factory method to
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="email"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public static User Create(Name name, Email email, Status status)
    {
        var user = new User(UserId.New(), name, email, status);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    /// <summary>
    /// Sets identity id.
    /// </summary>
    /// <param name="identityId"></param>
    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}
