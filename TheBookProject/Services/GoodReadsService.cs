using FluentValidation.Results;
using Newtonsoft.Json;
using NuGet.Protocol;
using TheBookProject.Db.Context;
using TheBookProject.Db.Entities;
using TheBookProject.Helpers;
using TheBookProject.Models;
using TheBookProject.Validators;
using Review = TheBookProject.Models.Review;

namespace TheBookProject.Services;

public class GoodReadsService : IGoodReadsService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly TheBookProjectDbContext _context;
    private readonly IBookService _bookService;
    private readonly IReviewService _reviewService;
 
    public GoodReadsService(IHttpClientFactory httpClientFactory, TheBookProjectDbContext context, IBookService bookService, IReviewService reviewService)
    {
         _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _httpClient = httpClientFactory.CreateClient("GoodReadsAPI");
        _context= context ?? throw new ArgumentNullException(nameof(context));
        _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
    }

    public async Task<RequestResponse> GetBookByURLAsync(string bookId)
    {
        try
        {
           
            var isDataValid = ValidateDataRequest(bookId);
          
            if (!isDataValid.Result) return isDataValid;
 
            return await GetBookFromReverseProxy(new GoodReadsRequest(){ BookId = bookId }); 
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
 
    
    public async Task<RequestResponse> AddBookByURLAsync(string bookId)
    {
        try
        {
            var isDataValid = ValidateDataRequest(bookId);
          
            if (!isDataValid.Result) return isDataValid;
 
            RequestResponse requestResponse = await GetBookFromReverseProxy(new GoodReadsRequest(){ BookId = bookId }); 
            
            if (!requestResponse.Result) return requestResponse;
 
            GoodReadsBooksResponse? bookInfo  = JsonConvert.DeserializeObject<GoodReadsBooksResponse>(requestResponse.Book);
            
            if (bookInfo == null ) return new RequestResponse("Google Books API Error: Not Found", null, false); 

            if (string.IsNullOrEmpty(bookInfo.details.isbn13))
            {
                return new RequestResponse("Good Reads API Error: The book doesn't have an isbn13 registered", null, false); 
            }
            else
            {
                if (_bookService.BookExists(bookInfo.details.isbn13))
                {
                    return new RequestResponse("Good Reads API Error: Book already exists in the database", null, false); 
                }
                
                BookDTO newBook = BuildBook(bookInfo, "Add");
                await  _bookService.AddBook(newBook);
                return new RequestResponse("Book added from Good Reads API to the database",  newBook.ToJson()); 
            }
 
        }
        catch (Exception e)
        {
            return new RequestResponse($"Good Reads API Error: Error while adding the book {e.Message}", null, false); 
        }
    }

    public async Task<RequestResponse> UpdateBookByURLAsync(string bookId)
    {
        try
        {
            var isDataValid = ValidateDataRequest(bookId);
          
            if (!isDataValid.Result) return isDataValid;
 
            RequestResponse requestResponse = await GetBookFromReverseProxy(new GoodReadsRequest(){ BookId = bookId }); 
            
            if (!requestResponse.Result) return requestResponse;
 
            GoodReadsBooksResponse? bookInfo  = JsonConvert.DeserializeObject<GoodReadsBooksResponse>(requestResponse.Book);
 
            if (bookInfo == null ) return new RequestResponse("Google Books API Error: Not Found", null, false); 

            if (string.IsNullOrEmpty(bookInfo.details.isbn13))
            {
                return new RequestResponse("Good Reads API Error: The book doesn't have an isbn13 registered", null, false); 
            }
            else
            {
                if (_bookService.BookExists(bookInfo.details.isbn13))
                {
                   
                    BookDTO newBook = BuildBook(bookInfo, "Update");
                    await  _bookService.UpdateBook(newBook);
                    return new RequestResponse("Book updated from Good Reads API", newBook.ToJson()); 
                }
                else
                {
                    return new RequestResponse("Good Reads API Error: Book doesn't exist in the database yet!", null, false); 
                }
           
            }
 
        }
        catch (Exception e)
        {
            return new RequestResponse($"Good Reads API Error: Error while adding the book {e.Message}", null, false); 
        }
    }

    private async Task<RequestResponse>  GetBookFromReverseProxy(GoodReadsRequest goodReadsRequest)
    {
        try
        {
      
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_httpClient.BaseAddress}getBookByID?bookID={goodReadsRequest.BookId}"),
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
            return new RequestResponse($"Good reads API Error: Error while getting the book {e.Message}", null, false); 
        }
    }

    private RequestResponse ValidateDataRequest(string bookId)
    {
        GoodReadsRequest goodReadsrequest = new()
        {
            BookId = bookId 
        };
            
        GoodReadsRequestValidator validator = new();

        ValidationResult result = validator.Validate(goodReadsrequest);
    
        if (!result.IsValid)
        {
            string errors = JsonConvert.SerializeObject(result.Errors.Select(error => error.ErrorMessage));
            return new RequestResponse(errors, null, false); 
        }
        return new RequestResponse(string.Empty, null); 
    }
    
    
    private BookDTO BuildBook(GoodReadsBooksResponse book, string action)
    {
        BookDTO newBook = new BookDTO();
        newBook.Origin = "GOOD READS API";
        newBook.ISBN = book.details.isbn13;
        newBook.Tittle = book.title;
        newBook.SubTittle = book.titleComplete;
        newBook.Publisher = book.details.publisher;
        newBook.Description = book.description;
        newBook.PageCount = book.details.numPages;
        newBook.Rating = book.stats.averageRating;
        newBook.RatingCount = book.stats.ratingsCount;
    
        if (action == "Add") 
        {
            if (book.reviews != null && book.reviews.Count > 0)
            {
                foreach (Review review in book.reviews)
                {
                    ReviewDTO newReview = new ReviewDTO();
                    newReview.ISBN = newBook.ISBN;
                    newReview.Rating = review.rating;
                    newReview.Origin = newBook.Origin;
                    newReview.Text = review.text;

                    _reviewService.AddReview(newReview);
                }
            }
        }
        return newBook;
        
    }
 
    
    
}