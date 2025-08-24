using Curiosity.Application.Abstractions.Messaging;

namespace Curiosity.Application.Users.RegisterCommand;

public sealed record RegisterUserCommand(
    string Email,
    string Name,
    string Password) : ICommand<string>;
