namespace TheBookProject.Models;

using System;
using System.Collections.Generic;

public class Language
{
    public string name { get; set; }
}

public class Genre
{
    public string name { get; set; }
    public string webUrl { get; set; }
}

public class BookDetails
{
    public int numPages { get; set; }
    public long publicationTime { get; set; }
    public string publisher { get; set; }
    public string isbn { get; set; }
    public string isbn13 { get; set; }
    public Language language { get; set; }
}

public class Author
{
    public string name { get; set; }
    public string description { get; set; }
    public string profileImageUrl { get; set; }
    public string webUrl { get; set; }
}

public class Award
{
    public string name { get; set; }
    public string webUrl { get; set; }
    public long awardedAt { get; set; }
    public string category { get; set; }
    public string designation { get; set; }
}

public class Place
{
    public string name { get; set; }
    public string countryName { get; set; }
    public string webUrl { get; set; }
}

public class Character
{
    public string name { get; set; }
    public string webUrl { get; set; }
}

public class BookStats
{
    public double averageRating { get; set; }
 
}

public class Review
{
    public string id { get; set; }
    public string text { get; set; }
    public int rating { get; set; }
    public int likeCount { get; set; }
    public int commentCount { get; set; }
}

public class GoodReadsBooksResponse
{
    public string title { get; set; }
    public string titleComplete { get; set; }
    public string description { get; set; }
    public string imageUrl { get; set; }
    public List<Genre> bookGenres { get; set; }
    public BookDetails details { get; set; }
    public Author author { get; set; }
    public List<Award> awardsWon { get; set; }
    public List<Place> places { get; set; }
    public List<Character> characters { get; set; }
    public BookStats stats { get; set; }
    public List<Review> reviews { get; set; }
}
