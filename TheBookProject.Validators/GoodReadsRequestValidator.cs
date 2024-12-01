using System.Text.RegularExpressions;
using FluentValidation;
using TheBookProject.Models;

namespace TheBookProject.Validators;

public class GoodReadsRequestValidator: AbstractValidator<GoodReadsRequest>
{
    public GoodReadsRequestValidator()
    {
 
        RuleFor(request => request.BookUrl)
            .NotEmpty().WithMessage("Goood Reads Book URL is required.")
            //.Matches(@"^https:\/\/www\.goodreads\.com\/book\/show\/\d+-[a-z0-9-]+$", RegexOptions.IgnoreCase)
            .WithMessage("Goodreads URL must follow the pattern 'https://www.goodreads.com/book/show/{id}-{slug}'.");
      
    }
}