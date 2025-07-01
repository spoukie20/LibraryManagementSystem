using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(UserRegisterDto registerDto);
        Task<User> LoginAsync(UserLoginDto loginDto);
        string GenerateJwtToken(User user);
    }
} 