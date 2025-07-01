using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Author { get; set; }
        public bool IsAvailable { get; set; }
        public int? BorrowedByUserId { get; set; }
    }
} 