using Microsoft.AspNetCore.Mvc;
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
    public async Task<string> Get(string ISBN)
    {
        return await _service.GetBookByISBNAsync(ISBN);
    }
    
    [HttpPost]
    public async Task<string> Post(string ISBN)
    {
        return await _service.SaveBookByISBNAsync(ISBN);
    }
}