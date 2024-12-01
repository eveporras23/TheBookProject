using TheBookProject.Db.Entities;

namespace TheBookProject.Models;

public class RequestResponse
{
    public bool Result { get; set; }
    public string Message { get; set; }
    public string Book { get; set; }

    public RequestResponse( string message, string book,bool result = true)
    {
        Result = result;
        Message = message;
        Book = book;
    }
}