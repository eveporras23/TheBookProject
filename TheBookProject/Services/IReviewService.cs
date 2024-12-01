using TheBookProject.Models;
using Review = TheBookProject.Db.Entities.Review;

namespace TheBookProject.Services;

public interface IReviewService
{
 
    public Task<List<Review>> GetAllByIsbn(string isbn, int? page);

    public Task<bool> AddReview(ReviewDTO review);
 
    public Task<bool> UpdateReview(ReviewDTO review);
 
    public  Task<bool> DeleteReview(Review review);
    public bool ReviewExists(int Id);
    public bool BookExists(string isbn);
    public ValueTask<Review?> FindReview(int Id);
    public string ValidateDataRequest(ReviewDTO review);
}