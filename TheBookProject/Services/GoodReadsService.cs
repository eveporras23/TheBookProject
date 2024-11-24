using TheBookProject.Helpers;

namespace TheBookProject.Services;

public class GoodReadsService : IGoodReadsService
{
    private readonly HttpClient _httpClient;


    public GoodReadsService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("GoodReadsAPI");
    }

    public async Task<string> GetBookByURLAsync(string goodReadsBookURL)
    {
        try
        {
            string goodReadsBookURLConverted = URIConverter.ConvertToString(goodReadsBookURL);
            
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_httpClient.BaseAddress}getBookByURL?url={goodReadsBookURLConverted}"),
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
}