using Curiosity.Application.Abstractions.Messaging;
using Curiosity.Application.Authentication;
using Curiosity.Application.Users.RegisterCommand;
using Curiosity.Domain.Abstractions;
using Curiosity.Domain.Users;

namespace Curiosity.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IAuthenticationService authenticationService,
    IUnitOfWork unitOfWork) : ICommandHandler<RegisterUserCommand, string>
{
    public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
            new Name(request.Name),
            new Email(request.Email),
            Status.Active);

        string identityId = await authenticationService.RegisterAsync(
            user,
            request.Password,
            cancellationToken);

        user.SetIdentityId(identityId);

        userRepository.Add(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id.Value;
    }
}
