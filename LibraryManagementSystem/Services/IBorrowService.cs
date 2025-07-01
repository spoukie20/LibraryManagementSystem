namespace LibraryManagementSystem.Services
{
    public interface IBorrowService
    {
        Task<bool> BorrowBook(int userId, int bookId);
        Task<bool> ReturnBook(int userId, int bookId);
    }
} 