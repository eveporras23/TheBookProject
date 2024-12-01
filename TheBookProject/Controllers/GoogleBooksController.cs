using Microsoft.AspNetCore.Mvc;
using TheBookProject.Models;
using TheBookProject.Services;

namespace TheBookProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GoogleBooksController : ControllerBase
{
   
    private IGoogleBooksService _service;
    private IBookService _bookService;

    public GoogleBooksController(IGoogleBooksService service)
    {
        _service = service;
    }
    
    [HttpGet("{isbn}")]
    public async Task<IActionResult> Get(string isbn)
    {
        RequestResponse requestResponse =  await _service.GetBookByISBNAsync(isbn.Trim());
        
        if (requestResponse.Result)
            return Ok(requestResponse);
        else
            return BadRequest(requestResponse);
    }
    
    [HttpPost("{isbn}")]
    public async Task<IActionResult> Post(string isbn)
    {
        RequestResponse requestResponse = await _service.AddBookByISBNAsync(isbn.Trim());
        
        if (requestResponse.Result)
            return Ok(requestResponse);
        else
           return BadRequest(requestResponse);
         
     
    }
    
    [HttpPut("{isbn}")]
    public async Task<IActionResult> Put(string isbn)
    {
        RequestResponse requestResponse = await _service.UpdateBookByISBNAsync(isbn.Trim());
        
        if (requestResponse.Result)
            return Ok(requestResponse);
        else
            return BadRequest(requestResponse);
    }
}