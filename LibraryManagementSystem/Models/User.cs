using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
    }
} 