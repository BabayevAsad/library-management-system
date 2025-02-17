using FluentValidation;

namespace Application.Books.Commands;

public class BookValidator<T> : AbstractValidator<T> where T : BookCommand
{
    protected BookValidator()
    {
            
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must be less than 100 characters.");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type is required.")
            .MaximumLength(100).WithMessage("Type must be less than 100 characters.");
 
        RuleFor(x => x.PageCount)
            .GreaterThan(0).WithMessage("Pagecount must be greater than 0.");
 
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
            
    }
    
}