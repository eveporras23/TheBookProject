using Microsoft.AspNetCore.Mvc;
using TheBookProject.Models;
using TheBookProject.Services;

namespace TheBookProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GoogleBooksController : ControllerBase
{
   
    private IGoogleBooksService _service;

    public GoogleBooksController(IGoogleBooksService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<string> Get(string isbn)
    {
        return await _service.GetBookByISBNAsync(isbn.Trim());
    }
    
    [HttpPost]
    public async Task<RequestResponse> Post(string isbn)
    {
        return await _service.AddBookByISBNAsync(isbn.Trim());
    }
    
    [HttpPut("{isbn}")]
    public async Task<RequestResponse> Put(string isbn)
    {
        return await _service.UpdateBookByISBNAsync(isbn.Trim());
    }
}