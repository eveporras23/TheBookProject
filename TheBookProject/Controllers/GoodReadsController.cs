using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheBookProject.Models;
using TheBookProject.Services;

namespace TheBookProject.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class GoodReadsController : ControllerBase
{
   
    private IGoodReadsService _service;

    public GoodReadsController(IGoodReadsService service)
    {
        _service = service;
    }
    
    [AllowAnonymous]
    [HttpGet("{bookId}")]
    public async  Task<IActionResult> Get(string bookId)
    {
       RequestResponse requestResponse =  await _service.GetBookByURLAsync(bookId.Trim());
        
        if (requestResponse.Result)
            return Ok(requestResponse);
        else
            return BadRequest(requestResponse);
    }
    
        
    [HttpPost("{bookId}")]
    public async  Task<IActionResult> Post(string bookId)
    {
 
        RequestResponse requestResponse =   await _service.AddBookByURLAsync(bookId.Trim());
        
        if (requestResponse.Result)
            return Ok(requestResponse);
        else
            return BadRequest(requestResponse);
    }
    
    [HttpPut("{bookId}")]
    public async  Task<IActionResult> Put(string bookId)
    {
        RequestResponse requestResponse = await _service.UpdateBookByURLAsync(bookId.Trim());
        
        if (requestResponse.Result)
            return Ok(requestResponse);
        else
            return BadRequest(requestResponse);
    }
}