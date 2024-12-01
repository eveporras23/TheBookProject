using FluentValidation;
using TheBookProject.Models;

namespace TheBookProject.Validators;

public class BookValidator: AbstractValidator<BookDTO>
{
    public BookValidator()
    {
 
        RuleFor(book => book.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Matches(@"^\d{13}$").WithMessage("ISBN must be exactly 13 digits.");
       
        RuleFor(book => book.Tittle)
            .NotEmpty().WithMessage("Tittle is required.");
        
        RuleFor(book => book.Origin)
            .NotEmpty().WithMessage("Origin is required.")
            .Matches("^(MANUAL)$")
            .WithMessage("The value Origin must be 'MANUAL'.");
 
    }
}