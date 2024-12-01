namespace TheBookProject.Models;

public class BookDTO
{
    public string Tittle { get; set; } = null!;

    public string? Author { get; set; }

    public string? Publisher { get; set; }

    public string? Category { get; set; }
 
    public string ISBN { get; set; } = null!;

    public string Origin { get; set; } = null!;

    public string? SubTittle { get; set; }

    public string? Description { get; set; }

    public string? PublishedDate { get; set; }

    public int? PageCount { get; set; }

    public string? Language { get; set; }

    public double? Rating { get; set; }

    public double? RatingCount { get; set; }
}