using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheBookProject.Db.Context;
using TheBookProject.Db.Entities;
using TheBookProject.Models;
using TheBookProject.Services;

namespace TheBookProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
         private IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Book
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook([FromQuery] int? page)
        {
 
            return await _bookService.GetAllBooks(page);
        }

        // GET: api/Book/5
        [AllowAnonymous]
        [HttpGet("{isbn}")]
        public async Task<ActionResult<Book>> GetBook(string isbn)
        {
            var book = await _bookService.GetAllByIsbn(isbn.Trim());

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Book/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{isbn}")]
        public async Task<IActionResult> PutBook(string isbn, BookDTO book)
        {
            if (isbn.Trim() != book.ISBN)
            {
                return BadRequest();
            }
            if (!_bookService.BookExists(isbn.Trim()))
            {
                return NotFound();
            }
            
            var hasErrors = _bookService.ValidateDataRequest(book);
            if (!string.IsNullOrEmpty(hasErrors))
            {
                return BadRequest(hasErrors);
            }
          
            await  _bookService.UpdateBook(book);

            return NoContent();
        } 

        // POST: api/Book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookDTO book)
        {
            if (_bookService.BookExists(book.ISBN))
            {
                return Conflict();
            }
            
            var hasErrors = _bookService.ValidateDataRequest(book);
            if (!string.IsNullOrEmpty(hasErrors))
            {
                return BadRequest(hasErrors);
            }
                
            await _bookService.AddBook(book);

            return Created();
        }

        // DELETE: api/Book/5
        [HttpDelete("{isbn}")]
        public async Task<IActionResult> DeleteBook(string isbn)
        {
           
            Book? book = await _bookService.FindBook(isbn.Trim());
           
            if (book == null)
            {
                return NotFound();
            }
        
            await _bookService.DeleteBook(book);
            
            return NoContent();
        }
 
    }
}
