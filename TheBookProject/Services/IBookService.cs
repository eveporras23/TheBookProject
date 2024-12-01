using TheBookProject.Db.Entities;

namespace TheBookProject.Services;

public interface IBookService
{
    public Task<List<Book>> GetAllBooks(int? page);
 
    public ValueTask<Book?> GetAllByIsbn(string isbn);

    public Task<bool> AddBook(Book book);
 
    public Task<bool> UpdateBook(Book book);
 
    public  Task<bool> DeleteBook(Book book);
    
    public bool BookExists(string isbn);
    
    public ValueTask<Book?> FindBook(string isbn);
    
 
}