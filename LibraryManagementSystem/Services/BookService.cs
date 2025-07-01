using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services
{

    public class BookService(LibraryContext _context) : IBookService, IBookAvailabilityService
    {
        public async Task<IEnumerable<Book>> GetAllBooksAsync() => await _context.Books.ToListAsync();

        public async Task<Book?> GetBookByIdAsync(int id) => await _context.Books.FindAsync(id);

        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    
        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsBookAvailableAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            return book != null && book.IsAvailable;
        }
    }
} 