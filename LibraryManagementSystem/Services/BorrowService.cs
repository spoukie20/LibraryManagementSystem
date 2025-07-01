using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class BorrowService(IBookService _bookService, IUserService _userService, INotificationService _notificationService) : IBorrowService
    {

        public async Task<bool> BorrowBook(int userId, int bookId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            var book = await _bookService.GetBookByIdAsync(bookId);
            if (user == null || book == null || !book.IsAvailable)
                return false;
            book.IsAvailable = false;
            book.BorrowedByUserId = user.Id;
            await _bookService.UpdateBookAsync(book);
            _notificationService.Notify($"User {user.Name} (ID: {user.Id}) borrowed book '{book.Title}' (ID: {book.Id})");
            return true;
        }

        public async Task<bool> ReturnBook(int userId, int bookId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            var book = await _bookService.GetBookByIdAsync(bookId);
            if (user == null || book == null || book.BorrowedByUserId != user.Id)
                return false;
            book.IsAvailable = true;
            book.BorrowedByUserId = null;
            await _bookService.UpdateBookAsync(book);
            _notificationService.Notify($"User {user.Name} (ID: {user.Id}) returned book '{book.Title}' (ID: {book.Id})");
            return true;
        }
    }
} 