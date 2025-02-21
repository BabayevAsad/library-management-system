using FluentValidation;

namespace Application.Library.Commands;

public class LibraryValidator<T> : AbstractValidator<T> where T : LibraryCommand
{
    protected LibraryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must be less than 100 characters.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Type is required.")
            .MaximumLength(100).WithMessage("Type must be less than 100 characters.");
 
        RuleFor(x => x.Count)
            .GreaterThan(0).WithMessage("Count must be greater than 0.");
    }
}