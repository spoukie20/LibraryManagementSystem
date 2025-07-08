using LibraryManagementSystem.Models;
using AutoMapper;

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

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
} 