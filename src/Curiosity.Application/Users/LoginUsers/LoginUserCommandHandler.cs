using Curiosity.Application.Abstractions.Messaging;
using Curiosity.Application.Authentication;
using Curiosity.Domain.Abstractions;

namespace Curiosity.Application.Users.LoginUsers;

internal sealed class LoginUserCommandHandler(IJwtService jwtService) : ICommandHandler<LoginUserCommand, AccessTokenResponse>
{
    public async Task<Result<AccessTokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        Result<string> result = await jwtService.GetAccessTokenAsync(
            request.Email,
            request.Password,
            cancellationToken);

        return result.IsFailure
            ? Result.Failure<AccessTokenResponse>(result.Error)
            : new AccessTokenResponse(result.Value);
    }
}
