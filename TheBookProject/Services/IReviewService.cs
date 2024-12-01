using TheBookProject.Db.Entities;

namespace TheBookProject.Services;

public interface IReviewService
{
 
    public Task<List<Review>> GetAllByIsbn(string isbn, int? page);

    public Task<bool> AddReview(Review review);
 
    public Task<bool> UpdateReview(Review review);
 
    public  Task<bool> DeleteReview(Review review);
    public bool ReviewExists(int Id);
    
    public ValueTask<Review?> FindReview(int Id);
}