using Curiosity.Application.Abstractions.Messaging;
using MediatR;

namespace Curiosity.Application.Users.GetUsers;

public sealed record GetUserQuery : IQuery<UserResponse>;
    

