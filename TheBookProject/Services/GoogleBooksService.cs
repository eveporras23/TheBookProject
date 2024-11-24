using Newtonsoft.Json.Linq;

namespace TheBookProject.Services;

public class GoogleBooksService : IGoogleBooksService
{
    private readonly HttpClient _httpClient;


    public GoogleBooksService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("GoogleBooksAPI");
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

    public async Task<string> SaveBookByISBNAsync(string isbn)
    {
        try
        {
            string responseBody = await GetBookByISBNAsync(isbn);

            JObject json = JObject.Parse(responseBody);
            string name = json["name"].ToString();
            int age = (int)json["age"];
            
            return responseBody;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}