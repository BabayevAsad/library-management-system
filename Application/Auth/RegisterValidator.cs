using FluentValidation;

namespace Application.Auth;

public class RegisterValidator<T> : AbstractValidator<T> where T : RegisterCommand
{
    protected RegisterValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage("Username must be less than 50 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.RoleId).GreaterThan(0).WithMessage("RoleId must be a valid role.");
    }
}