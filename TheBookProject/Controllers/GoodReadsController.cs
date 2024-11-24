using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    
    [HttpGet]
    public async Task<string> Get(string goodReadsBookUrl)
    {
        return await _service.GetBookByURLAsync(goodReadsBookUrl);
    }
}