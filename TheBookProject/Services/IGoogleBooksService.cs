using TheBookProject.Models;

namespace TheBookProject.Services;

public interface IGoogleBooksService
{
    Task<RequestResponse> GetBookByISBNAsync(string isbn);

    Task<RequestResponse> AddBookByISBNAsync(string isbn);
    Task<RequestResponse> UpdateBookByISBNAsync(string isbn);
}