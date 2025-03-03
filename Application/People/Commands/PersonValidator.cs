using Api.People;
using FluentValidation;

namespace Application.People.Commands;

public class PersonValidator<T> : AbstractValidator<T> where T : PersonCommand
{
    protected PersonValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must be less than 100 characters.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .MaximumLength(100).WithMessage("Surname must be less than 100 characters.");
        
        RuleFor(x => x.FatherName)
            .NotEmpty().WithMessage("FatherName is required.")
            .MaximumLength(100).WithMessage("FatherName must be less than 100 characters.");
            
        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("BirthDate is required.")
            .LessThan(DateTime.Today).WithMessage("BirthDate must be in the past.");
            
        RuleFor(x => x.FinNumber)
            .NotEmpty().WithMessage("FinNumber is required.")
            .Matches(@"^\d{5}-\d{5}$").WithMessage("FinNumber must be in the format '12345-67890'.");
        
        RuleFor(x => x.GenderId)
            .NotEmpty().WithMessage("Gender is required.") 
            .Must(genderId => GenderHelper.GetById(genderId) != null)
            .WithMessage("GenderId must be a valid gender.");
    }
}