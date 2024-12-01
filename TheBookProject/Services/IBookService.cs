using TheBookProject.Db.Entities;
using TheBookProject.Models;

namespace TheBookProject.Services;

public interface IBookService
{
    public Task<List<Book>> GetAllBooks(int? page);
 
    public ValueTask<Book?> GetAllByIsbn(string isbn);

    public Task<bool> AddBook(BookDTO book);
 
    public Task<bool> UpdateBook(BookDTO book);
 
    public  Task<bool> DeleteBook(Book book);
    
    public bool BookExists(string isbn);
    
    public ValueTask<Book?> FindBook(string isbn);
    
    public string ValidateDataRequest(BookDTO review);
}