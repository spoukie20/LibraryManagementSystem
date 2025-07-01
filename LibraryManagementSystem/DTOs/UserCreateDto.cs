using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.DTOs
{
    public class UserCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public User ToUser() => new User
        {
            Name = Name
        };
    }
} 