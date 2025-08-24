using Curiosity.Application.Abstractions.Messaging;

namespace Curiosity.Application.Users.LoginUsers;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<AccessTokenResponse>;
