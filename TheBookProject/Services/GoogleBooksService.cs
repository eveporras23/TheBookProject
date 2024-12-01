using FluentValidation.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using TheBookProject.Db.Context;
using TheBookProject.Db.Entities;
using TheBookProject.Helpers;
using TheBookProject.Models;
using TheBookProject.Validators;


namespace TheBookProject.Services;

public class GoogleBooksService : IGoogleBooksService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly TheBookProjectDbContext _context;
    private readonly IBookService _bookService;


    public GoogleBooksService(IHttpClientFactory httpClientFactory, TheBookProjectDbContext context, IBookService bookService)
    { 
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _httpClient = httpClientFactory.CreateClient("GoogleBooksAPI");
        _context= context ?? throw new ArgumentNullException(nameof(context));
        _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
    }

    public async Task<RequestResponse> GetBookByISBNAsync(string isbn)
    {
        try
        { 
            var isDataValid = ValidateDataRequest(isbn);
          
            if (!isDataValid.Result) return isDataValid;
            
            
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_httpClient.BaseAddress}/books/v1/volumes?q=isbn:{isbn}"),
            };
            
            var responseBody = string.Empty; 
            
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            
            return new RequestResponse(string.Empty, responseBody); 
        }
        catch (Exception e)
        {
            return new RequestResponse($"Google books API Error: Error while getting the book {e.Message}", null, false); 
        }
    }

    public async Task<RequestResponse> AddBookByISBNAsync(string isbn)
    {
        try
        {
            var isDataValid = ValidateDataRequest(isbn);
          
            if (!isDataValid.Result) return isDataValid;
            
            if (_bookService.BookExists(isbn))
            {
                return new RequestResponse("Book already exists in the database", null, false); 
            }
            
            var requestResponse = await GetBookByISBNAsync(isbn);
             
            if (!requestResponse.Result) return requestResponse;       
            
            GoogleBooksResponse? bookInfo  = JsonConvert.DeserializeObject<GoogleBooksResponse>(requestResponse.Book);;
            
            if (bookInfo == null || bookInfo.Items == null) return new RequestResponse("Google Books API Error: Not Found", null,false); 
            
            if (bookInfo.Items.Count > 0)
            {
                var bookItem = bookInfo.Items[0];
                BookDTO newBook = BuildBook(bookItem, isbn);
                await  _bookService.AddBook(newBook);
                return new RequestResponse("Book added from Google Books API", newBook.ToJson()); 
            }
            else
            {
                return new RequestResponse("Google Books API Error: Not Found", null,false); 
            }
 
        }
        catch (Exception e)
        {
            return new RequestResponse($"Google books API Error: Error while adding the book {e.Message}", null, false); 
        }
    }
    
    public async Task<RequestResponse> UpdateBookByISBNAsync(string isbn)
    {
        try
        {
            
            var isDataValid = ValidateDataRequest(isbn);
          
            if (!isDataValid.Result) return isDataValid;
            
            if (!_bookService.BookExists(isbn))
            {
                return new RequestResponse("Book doesn't exist from Google Books API", null,false); 
            }
            
            var requestResponse = await GetBookByISBNAsync(isbn);
             
            if (!requestResponse.Result) return requestResponse;       
            
            GoogleBooksResponse? bookInfo  = JsonConvert.DeserializeObject<GoogleBooksResponse>(requestResponse.Book);;
            
            if (bookInfo == null || bookInfo.Items == null) return new RequestResponse("Google Books API Error: Not Found", null,false); 
            
            if (bookInfo.Items.Count > 0)
            {
                var bookItem = bookInfo.Items[0];
                BookDTO newBook = BuildBook(bookItem,isbn);
                await  _bookService.UpdateBook(newBook);
                return new RequestResponse("Book updated from Google Books API",  newBook.ToJson()); 
            }
            else
            {
                return new RequestResponse("Google Books API Error: Not Found", null,false); 
            }
 
        }
        catch (Exception e)
        {
            return new RequestResponse($"Google books API Error: Error while updating the book {e.Message}", null, false); 
 
        }
    }
    
    private RequestResponse ValidateDataRequest(string isbn)
    {
        GoogleBooksRequest googleBooksRequest = new()
        {
            ISBN = isbn 
        };
            
        GoogleBooksRequestValidator validator = new();

        ValidationResult result = validator.Validate(googleBooksRequest);
    
        if (!result.IsValid)
        {
            string errors = JsonConvert.SerializeObject(result.Errors.Select(error => error.ErrorMessage));
            return new RequestResponse(errors, null, false); 
        }
        return new RequestResponse(string.Empty, null); 
    }

    private BookDTO BuildBook(BookItem book, string isbn)
    {
        BookDTO newBook = new BookDTO();
        newBook.Origin = "GOOGLE BOOKS API";
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