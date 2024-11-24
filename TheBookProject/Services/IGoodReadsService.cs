namespace TheBookProject.Services;

public interface IGoodReadsService
{
    Task<string> GetBookByURLAsync(string goodReadsBookURL);
    
    
}