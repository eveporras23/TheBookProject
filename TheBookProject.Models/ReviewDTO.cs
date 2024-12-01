namespace TheBookProject.Models;

public class ReviewDTO
{
    public int Id { get; set; }
    public string ISBN { get; set; } = null!;

    public string? Text { get; set; }

    public string? Origin { get; set; }

    public double? Rating { get; set; }
}