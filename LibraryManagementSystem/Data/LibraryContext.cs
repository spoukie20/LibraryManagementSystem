using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
} 