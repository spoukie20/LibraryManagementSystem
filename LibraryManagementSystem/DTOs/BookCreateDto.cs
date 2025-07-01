using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.DTOs
{
    public class BookCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        public Book ToBook() => new Book
        {
            Title = Title,
            Author = Author,
            IsAvailable = true
        };
    }
} 