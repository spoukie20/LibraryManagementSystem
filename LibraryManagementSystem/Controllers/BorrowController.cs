using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BorrowController(IBorrowService _borrowService, IBookService _bookService) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Borrow(int userId, int bookId)
        {
            try
            {
                var success = await _borrowService.BorrowBook(userId, bookId);
                if (!success) return BadRequest("Book is not available or user/book not found.");
                var book = await _bookService.GetBookByIdAsync(bookId);
                return Ok(book);
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

        [HttpPost("return")]
        public async Task<IActionResult> Return(int userId, int bookId)
        {
            try
            {
                var success = await _borrowService.ReturnBook(userId, bookId);
                if (!success) return BadRequest("Book was not borrowed by this user or not found.");
                var book = await _bookService.GetBookByIdAsync(bookId);
                return Ok(book);
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