using TheBookProject.Entities;

namespace TheBookProject.Models;

public class ResultRequest
{
    public string RequestResult { get; set; }
    public Book Book { get; set; }

    public ResultRequest(string requestResult, Book book)
    {
        RequestResult = requestResult;
        Book = book;
    }
}