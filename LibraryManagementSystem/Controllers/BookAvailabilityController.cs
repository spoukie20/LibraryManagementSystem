using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookAvailabilityController(IBookAvailabilityService _availabilityService) : ControllerBase
    {
        [HttpGet("{bookId}")]
        public async Task<IActionResult> IsAvailable(int bookId)
        {
            try
            {
                var isAvailable = await _availabilityService.IsBookAvailableAsync(bookId);
                if (!isAvailable)
                {
                    return NotFound(new { bookId, isAvailable = false, message = "Book not found or not available." });
                }
                return Ok(new { bookId, isAvailable = true, message = "Book is available." });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "A database error occurred.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }
    }
} 