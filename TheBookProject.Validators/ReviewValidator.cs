using FluentValidation;
using TheBookProject.Models;

namespace TheBookProject.Validators;

public class ReviewValidator: AbstractValidator<ReviewDTO>
{
    public ReviewValidator()
    {
 
        RuleFor(review => review.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Matches(@"^\d{13}$").WithMessage("ISBN must be exactly 13 digits.");
        RuleFor(review => review.Origin)
            .NotEmpty().WithMessage("Origin is required.")
            .Matches("^(MANUAL|GOOD READS)$")
            .WithMessage("The value Origin must be 'MANUAL' or 'GOOD READS'.");
        RuleFor(review => review.Text)
            .NotEmpty().WithMessage("Text is required.")
            .Must(HaveMoreThan100Words)
            .WithMessage("The Text must contain at least 100 words.");
        RuleFor(review => review.Rating)
            .NotEmpty().WithMessage("Rating is required.")
            .Must(rating => rating.Value <= 5 && rating.Value >= 0)
            .WithMessage("Rating must be between 0 and 5.");
 
    }
    
    private bool HaveMoreThan100Words(string? content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return false;
 
        var wordCount = content.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        return wordCount >= 100;
    }
}