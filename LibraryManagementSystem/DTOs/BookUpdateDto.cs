using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.DTOs
{
    public class BookUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Author { get; set; }
        public bool IsAvailable { get; set; }
        public int? BorrowedByUserId { get; set; }

        public void UpdateBook(Book book)
        {
            book.Title = Title;
            book.Author = Author;
            book.IsAvailable = IsAvailable;
            book.BorrowedByUserId = BorrowedByUserId;
        }
    }
} 