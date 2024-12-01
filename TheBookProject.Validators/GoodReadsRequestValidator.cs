using System.Text.RegularExpressions;
using FluentValidation;
using TheBookProject.Models;

namespace TheBookProject.Validators;

public class GoodReadsRequestValidator: AbstractValidator<GoodReadsRequest>
{
    public GoodReadsRequestValidator()
    {
 
        RuleFor(request => request.BookId)
            .NotEmpty().WithMessage("Goood Reads Book URL is required.")
            .Matches(@"^\d$").WithMessage("Goodreads Book Id must be a number.");
  
    }
}