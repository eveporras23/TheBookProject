using FluentValidation;
using TheBookProject.Models;

namespace TheBookProject.Validators;

public class GoogleBooksRequestValidator: AbstractValidator<GoogleBooksRequest>
{
    public GoogleBooksRequestValidator()
    {
 
        RuleFor(book => book.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Matches(@"^\d{13}$").WithMessage("ISBN must be exactly 13 digits.");
      
    }
}