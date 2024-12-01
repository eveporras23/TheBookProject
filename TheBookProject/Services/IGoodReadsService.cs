using TheBookProject.Models;

namespace TheBookProject.Services;

public interface IGoodReadsService
{
    Task<RequestResponse> GetBookByURLAsync(string goodReadsBookURL);

    public  Task<RequestResponse> AddBookByURLAsync(string bookURL);
    
    public  Task<RequestResponse> UpdateBookByURLAsync(string bookURL);
}