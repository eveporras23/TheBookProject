namespace TheBookProject.Models;

public class GoogleBooksResponse
{
    public string Kind { get; set; }
    public int TotalItems { get; set; }
    public List<BookItem> Items { get; set; }
}

public class BookItem
{
    public string Kind { get; set; }
    public string SelfLink { get; set; }
    public VolumeInfo VolumeInfo { get; set; }
 
}

public class VolumeInfo
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public List<string> Authors { get; set; }
    public string Publisher { get; set; }
    public string PublishedDate { get; set; }
    public string Description { get; set; }
 
    public int PageCount { get; set; }
    public string PrintType { get; set; }
    public List<string> Categories { get; set; }
    public string Language { get; set; }
}
 
 