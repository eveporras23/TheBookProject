using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using TheBookProject.Db.Context;
using TheBookProject.Db.Entities;
using TheBookProject.Models;
using TheBookProject.Validators;
using Review = TheBookProject.Db.Entities.Review;

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
       return _context.Books
           .Include(r=>r.Reviews)
           .Skip((page ?? 1) * 20).Take(20).ToListAsync();
    }
    public ValueTask<Book?> GetAllByIsbn(string isbn)
    {
        return  _context.Books.FindAsync(isbn);
    }

    public async Task<bool> AddBook(BookDTO bookDto)
    {
        if (!BookExists(bookDto.ISBN))
        {
 
            _context.Books.Add(BuildBook(bookDto));
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> UpdateBook(BookDTO bookDto)
    {
        if (BookExists(bookDto.ISBN))
        {
            _context.Entry(BuildBook(bookDto)).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        return false; 
    }
    
    public async Task<bool> DeleteBook(Book book)
    {
        Book? bookToDelete = await _context.Books.Include(b => b.Reviews).FirstOrDefaultAsync(b => b.ISBN == book.ISBN);
        if (bookToDelete != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();  
            return true; 
        }
       return false; 
    }
    
    public bool BookExists(string isbn)
    {
        return _context.Books.Any(e => e.ISBN == isbn);
    }
    
    public ValueTask<Book?> FindBook(string isbn)
    {
        return _context.Books.FindAsync(isbn);
    }
    
    public string ValidateDataRequest(BookDTO book)
    {
       
        BookValidator validator = new();

        ValidationResult result = validator.Validate(book);
    
        if (!result.IsValid)
        {
            string errors = JsonConvert.SerializeObject(result.Errors.Select(error => error.ErrorMessage));
            return errors; 
        }
        return string.Empty; 
    }

    private Book BuildBook(BookDTO bookDto)
    {
        Book book = new Book();
        book.Tittle = bookDto.Tittle;
        book.Author = bookDto.Author;
        book.Publisher = bookDto.Publisher;
        book.Category = bookDto.Category;
        book.ISBN = bookDto.ISBN;
        book.Origin = bookDto.Origin;
        book.SubTittle = bookDto.SubTittle;
        book.Description = bookDto.Description;
        book.PublishedDate = bookDto.PublishedDate;
        book.PageCount = bookDto.PageCount;
        book.Rating = bookDto.Rating;
        book.RatingCount = bookDto.RatingCount;
        return book;
    }
}