using Microsoft.EntityFrameworkCore;
using TheBookProject.Db.Context;
using TheBookProject.Db.Entities;

namespace TheBookProject.Services;

public class BookService : IBookService
{
   private readonly TheBookProjectDbContext _context;

    public BookService(TheBookProjectDbContext context)
    {
        _context =  context ?? throw new ArgumentNullException(nameof(context));
    }
    public Task<List<Book>> GetAllBooks(int? page)
    {
       return _context.Books.Skip((page ?? 1) * 20).Take(20).ToListAsync();
    }
    public ValueTask<Book?> GetAllByIsbn(string isbn)
    {
        return  _context.Books.FindAsync(isbn);
    }

    public async Task<bool> AddBook(Book book)
    {
        if (!BookExists(book.ISBN))
        {
            _context.Books.Add(book);
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
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true; 
    }
    
    public bool BookExists(string isbn)
    {
        return _context.Books.Any(e => e.ISBN == isbn);
    }
    
    public ValueTask<Book?> FindBook(string isbn)
    {
        return _context.Books.FindAsync(isbn);
    }
}