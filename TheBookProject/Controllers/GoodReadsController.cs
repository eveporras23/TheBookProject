using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheBookProject.Models;
using TheBookProject.Services;

namespace TheBookProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GoodReadsController : ControllerBase
{
   
    private IGoodReadsService _service;

    public GoodReadsController(IGoodReadsService service)
    {
        _service = service;
    }
    
    [HttpGet("{bookURL}")]
    public async  Task<IActionResult> Get(string bookURL)
    {
       RequestResponse requestResponse =  await _service.GetBookByURLAsync(bookURL.Trim());
        
        if (requestResponse.Result)
            return Ok(requestResponse);
        else
            return BadRequest(requestResponse);
    }
    
        
    [HttpPost("{bookURL}")]
    public async  Task<IActionResult> Post(string bookURL)
    {
 
        RequestResponse requestResponse =   await _service.AddBookByURLAsync(bookURL.Trim());
        
        if (requestResponse.Result)
            return Ok(requestResponse);
        else
            return BadRequest(requestResponse);
    }
    
    [HttpPut("{bookURL}")]
    public async  Task<IActionResult> Put(string bookURL)
    {
        RequestResponse requestResponse = await _service.UpdateBookByURLAsync(bookURL.Trim());
        
        if (requestResponse.Result)
            return Ok(requestResponse);
        else
            return BadRequest(requestResponse);
    }
}