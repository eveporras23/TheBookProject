namespace TheBookProject.Services;

public interface IGoogleBooksService
{
    Task<string> GetBookByISBNAsync(string isbn);

    Task<string> SaveBookByISBNAsync(string isbn);
}