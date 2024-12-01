using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBookProject.Db.Context;
using TheBookProject.Db.Entities;
using TheBookProject.Services;

namespace TheBookProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
           _reviewService = reviewService;
        }

        // GET: api/Review
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews(string isbn, [FromQuery] int? page)
        {
            return await _reviewService.GetAllByIsbn(isbn, page);
        }

        // GET: api/Review/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _reviewService.FindReview(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // PUT: api/Review/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }
 
            if (!_reviewService.ReviewExists(id))
            {
                return NotFound();
            }

            var hasErrors = _reviewService.ValidateDataRequest(review);
            if (!string.IsNullOrEmpty(hasErrors))
            {
                return BadRequest(hasErrors);
            }
          
            await  _reviewService.UpdateReview(review);

            return NoContent();
        }

        // POST: api/Review
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            if (_reviewService.ReviewExists(review.Id))
            {
                return Conflict();
            }
            
            var hasErrors = _reviewService.ValidateDataRequest(review);
            if (!string.IsNullOrEmpty(hasErrors))
            {
                return BadRequest(hasErrors);
            }
                
            await _reviewService.AddReview(review);

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        // DELETE: api/Review/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            Review? book = await _reviewService.FindReview(id);
           
            if (book == null)
            {
                return NotFound();
            }
        
            await _reviewService.DeleteReview(book);
            
            return NoContent();
        }

      
    }
}
