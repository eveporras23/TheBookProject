namespace TheBookProject.Models;

using System;
using System.Collections.Generic;
 
public class BookDetails
{
    public int numPages { get; set; }
    public long publicationTime { get; set; }
    public string publisher { get; set; }
    public string isbn { get; set; }
    public string isbn13 { get; set; }
}
 
public class BookStats
{
    public double averageRating { get; set; }
    public double ratingsCount { get; set; }
}

public class Review
{
    public string id { get; set; }
    public string text { get; set; }
    public int rating { get; set; }
 
}

public class GoodReadsBooksResponse
{
    public string title { get; set; }
    public string titleComplete { get; set; }
    public string description { get; set; }
    public BookDetails details { get; set; }
    public BookStats stats { get; set; }
    public List<Review> reviews { get; set; }
}
