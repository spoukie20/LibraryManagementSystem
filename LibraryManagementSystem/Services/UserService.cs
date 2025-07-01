using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services
{
    public class UserService(LibraryContext _context) : IUserService
    {

        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _context.Users.ToListAsync();
        public async Task<User?> GetUserByIdAsync(int id) => await _context.Users.FindAsync(id);
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
} 