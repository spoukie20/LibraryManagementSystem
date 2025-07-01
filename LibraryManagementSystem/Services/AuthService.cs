using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LibraryManagementSystem.Services
{
    public class AuthService( Data.LibraryContext _context, Configuration _configuration) : IAuthService
    {

        public async Task<User> RegisterAsync(UserRegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
                throw new Exception("Username already exists");

            var user = new User
            {
                Name = registerDto.Name,
                Username = registerDto.Username,
                PasswordHash = HashPassword(registerDto.Password)
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> LoginAsync(UserLoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);
            if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
                throw new Exception("Invalid username or password");
            return user;
        }

        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }
    }
} 