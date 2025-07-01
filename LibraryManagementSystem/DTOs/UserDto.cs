using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UserDto(User user)
        {
            Id = user.Id;
            Name = user.Name;
        }
    }
} 