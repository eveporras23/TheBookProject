using Microsoft.EntityFrameworkCore;
using TheBookProject.Context;
using TheBookProject.Entities;

namespace TheBookProject.Services;

public class BookService
{
    private static BookService _instance;
    private readonly TheBookProjectDbContext _context;

    public BookService(TheBookProjectDbContext context)
    {
        _context = context;
    }

    public static BookService Instance(TheBookProjectDbContext dbContext)
    {
        _instance = new BookService(dbContext);
        return _instance;
    }
 
    public Task<List<Book>> GetAllBooks()
    {
       return _context.Book.ToListAsync();
    }
    public ValueTask<Book?> GetAllByIsbn(string isbn)
    {
        return  _context.Book.FindAsync(isbn);
    }

    public async Task<bool> AddBook(Book book)
    {
        if (!BookExists(book.ISBN))
        {
            _context.Book.Add(book);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> UpdateBook(Book book)
    {
        if (BookExists(book.ISBN))
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        return false; 
    }
    
    public async Task<bool> DeleteBook(Book book)
    {
        _context.Book.Remove(book);
        await _context.SaveChangesAsync();
        return true; 
    }
    
    public bool BookExists(string isbn)
    {
        return _context.Book.Any(e => e.ISBN == isbn);
    }
    
    public ValueTask<Book?> FindBook(string isbn)
    {
        return _context.Book.FindAsync(isbn);;
    }
}