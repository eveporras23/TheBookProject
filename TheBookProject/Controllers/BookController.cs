using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBookProject.Context;
using TheBookProject.Entities;
using TheBookProject.Services;

namespace TheBookProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly TheBookProjectDbContext _context;
        private readonly BookService _bookServiceInstance;

        public BookController(TheBookProjectDbContext context)
        {
            _context = context;
            _bookServiceInstance = BookService.Instance(_context);
        }

        // GET: api/Book
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
            
            return await _bookServiceInstance.GetAllBooks();
        }

        // GET: api/Book/5
        [AllowAnonymous]
        [HttpGet("{isbn}")]
        public async Task<ActionResult<Book>> GetBook(string isbn)
        {
            var book = await _bookServiceInstance.GetAllByIsbn(isbn.Trim());

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Book/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{isbn}")]
        public async Task<IActionResult> PutBook(string isbn, Book book)
        {
            if (isbn.Trim() != book.ISBN)
            {
                return BadRequest();
            }
            if (!_bookServiceInstance.BookExists(isbn.Trim()))
            {
                return NotFound();
            }
          
            await  _bookServiceInstance.UpdateBook(book);

            return NoContent();
        } 

        // POST: api/Book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            if (_bookServiceInstance.BookExists(book.ISBN))
            {
                return Conflict();
            }
                
            await _bookServiceInstance.AddBook(book);

            return CreatedAtAction("GetBook", new { id = book.ISBN }, book);
        }

        // DELETE: api/Book/5
        [HttpDelete("{isbn}")]
        public async Task<IActionResult> DeleteBook(string isbn)
        {
           
            Book? book = await _bookServiceInstance.FindBook(isbn.Trim());
           
            if (book == null)
            {
                return NotFound();
            }
        
            await _bookServiceInstance.DeleteBook(book);
            
            return NoContent();
        }
 
    }
}
