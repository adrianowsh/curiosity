using Curiosity.Application.Abstractions.Messaging;
using Curiosity.Domain.Abstractions;
using Curiosity.Domain.Users;

namespace Curiosity.Application.Users.RegisterCommand;

internal sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RegisterUserCommand, string>
{
    public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
            new Name(request.Name),
            new Email(request.Email),
            Status.Active);

        userRepository.Add(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id.Value;
    }
}
