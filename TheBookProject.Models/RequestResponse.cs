using TheBookProject.Db.Entities;

namespace TheBookProject.Models;

public class RequestResponse
{
    public string Response { get; set; }
    public Book Book { get; set; }

    public RequestResponse(string response, Book book)
    {
        Response = response;
        Book = book;
    }
}