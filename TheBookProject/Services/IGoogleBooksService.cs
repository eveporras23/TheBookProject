using TheBookProject.Models;

namespace TheBookProject.Services;

public interface IGoogleBooksService
{
    Task<string> GetBookByISBNAsync(string isbn);

    Task<ResultRequest> SaveBookByISBNAsync(string isbn);
}