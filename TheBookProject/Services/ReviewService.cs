using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TheBookProject.Db.Context;
using TheBookProject.Models;
using TheBookProject.Validators;
using Review = TheBookProject.Db.Entities.Review;

namespace TheBookProject.Services;

public class ReviewService : IReviewService
{
    
    private readonly TheBookProjectDbContext _context;

    public ReviewService(TheBookProjectDbContext context)
    {
        _context =  context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public Task<List<Review>> GetAllByIsbn(string isbn, int? page)
    {
        return _context.Reviews.Where(r=> r.ISBN== isbn).Skip((page ?? 1) * 20).Take(20).ToListAsync();
    }

    public async Task<bool> AddReview(ReviewDTO reviewDto)
    {
        if (!ReviewExists(reviewDto.Id))
        {
            Review review = new Review();
            review.ISBN = reviewDto.ISBN;
            review.Origin = reviewDto.Origin;
            review.Text = reviewDto.Text;
            review.Rating = reviewDto.Rating;
            
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> UpdateReview(ReviewDTO reviewDto)
    {
        if (ReviewExists(reviewDto.Id))
        {
            Review review = new Review();
            review.Id = reviewDto.Id;
            review.ISBN = reviewDto.ISBN;
            review.Origin = reviewDto.Origin;
            review.Text = reviewDto.Text;
            review.Rating = reviewDto.Rating;
            
            _context.Entry(review).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        return false; 
    }

    public async Task<bool> DeleteReview(Review review)
    {
    
        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
        return true; 
    }
    
    public bool ReviewExists(int Id)
    {
        return _context.Reviews.Any(e => e.Id == Id);
    }

    public ValueTask<Review?> FindReview(int Id)
    {
        return _context.Reviews.FindAsync(Id);
    }
    
    public string ValidateDataRequest(ReviewDTO reviewDto)
    {
       
        ReviewValidator validator = new();

        ValidationResult result = validator.Validate(reviewDto);
    
        if (!result.IsValid)
        {
            string errors = JsonConvert.SerializeObject(result.Errors.Select(error => error.ErrorMessage));
            return errors; 
        }
        return string.Empty; 
    }
}