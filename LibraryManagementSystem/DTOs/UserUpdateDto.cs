using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.DTOs
{
    public class UserUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public void UpdateUser(User user)
        {
            user.Name = Name;
        }
    }
} 