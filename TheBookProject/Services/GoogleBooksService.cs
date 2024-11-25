using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TheBookProject.Context;
using TheBookProject.Entities;
using TheBookProject.Helpers;
using TheBookProject.Models;

namespace TheBookProject.Services;

public class GoogleBooksService : IGoogleBooksService
{
    private readonly HttpClient _httpClient;
    private readonly BookService _bookServiceInstance;


    public GoogleBooksService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("GoogleBooksAPI");
        _bookServiceInstance = new BookService(new TheBookProjectDbContext());
    }

    public async Task<string> GetBookByISBNAsync(string query)
    {
        try
        { 
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_httpClient.BaseAddress}/books/v1/volumes?q=isbn:{query}"),
            };
            
            var responseBody = string.Empty; 
            
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            
            return responseBody;
        }
        catch (Exception e)
        {
           
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ResultRequest> AddBookByISBNAsync(string isbn)
    {
        try
        {
            if (_bookServiceInstance.BookExists(isbn))
            {
                return new ResultRequest("Book already added from Google Books API", null); 
            }
            
            var responseBody = await GetBookByISBNAsync(isbn);
             
            GoogleBooksResponse? bookInfo  = JsonConvert.DeserializeObject<GoogleBooksResponse>(responseBody);;
            
            if (bookInfo == null || bookInfo.Items == null) return new ResultRequest("Google Books API Error: Not Found", null); 
            
            if (bookInfo.Items.Count > 0)
            {
                var bookItem = bookInfo.Items[0];
                Book newBook = BuildBook(bookItem, isbn);
                await  _bookServiceInstance.AddBook(newBook);
                return new ResultRequest("Book added from Google Books API", newBook); 
            }
            else
            {
                return new ResultRequest("Google Books API Error: Not Found", null); 
            }
 
        }
        catch (Exception e)
        {
 
            throw;
        }
    }
    
    public async Task<ResultRequest> UpdateBookByISBNAsync(string isbn)
    {
        try
        {
            if (!_bookServiceInstance.BookExists(isbn))
            {
                return new ResultRequest("Book doesn't exist from Google Books API", null); 
            }
            
            var responseBody = await GetBookByISBNAsync(isbn);
             
            GoogleBooksResponse? bookInfo  = JsonConvert.DeserializeObject<GoogleBooksResponse>(responseBody);;
            
            if (bookInfo == null || bookInfo.Items == null) return new ResultRequest("Google Books API Error: Not Found", null); 
            
            if (bookInfo.Items.Count > 0)
            {
                var bookItem = bookInfo.Items[0];
                Book newBook = BuildBook(bookItem,isbn);
                await  _bookServiceInstance.UpdateBook(newBook);
                return new ResultRequest("Book updated from Google Books API", newBook); 
            }
            else
            {
                return new ResultRequest("Google Books API Error: Not Found", null); 
            }
 
        }
        catch (Exception e)
        {
 
            throw;
        }
    }

    private Book BuildBook(BookItem book, string isbn)
    {
        Book newBook = new Book();
        newBook.Origin = "Google Books API";
        newBook.ISBN = isbn;
        newBook.Tittle = book.VolumeInfo.Title;
        newBook.SubTittle = book.VolumeInfo.Subtitle;
        newBook.Author = (book.VolumeInfo.Authors != null) ? string.Join(", ", book.VolumeInfo.Authors) : null;
        newBook.Publisher = book.VolumeInfo.Publisher;
        newBook.Description = book.VolumeInfo.Description;
        newBook.PublishedDate = book.VolumeInfo.PublishedDate;
        newBook.PageCount = book.VolumeInfo.PageCount;
        newBook.Category = (book.VolumeInfo.Categories != null) ? string.Join(", ", book.VolumeInfo.Categories) : null;
        newBook.Language = book.VolumeInfo.Language;
        
        return newBook;
    }
    
}