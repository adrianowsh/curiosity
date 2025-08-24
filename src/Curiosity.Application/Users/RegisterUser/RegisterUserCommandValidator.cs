using FluentValidation;

namespace Curiosity.Application.Users.RegisterCommand;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.");

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email address.");

        RuleFor(c => c.Password).NotEmpty().WithMessage("Password is required.");
    }
}
