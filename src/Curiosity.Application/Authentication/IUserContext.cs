namespace Curiosity.Application.Authentication;

public interface IUserContext
{
    string UserId { get; }

    string IdentityId { get; }
}
